using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using HR.Application;
using HR.Application.Attendance;
using HR.Application.Services;
using HR.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HR.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AttendanceService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AttendanceDto>> GetAllAsync(AttendanceFilterDto filter)
        {
            var attendances = await _uow.Attendances.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _uow.Attendances.GetByIdAsync(id);
            return attendance == null ? null : _mapper.Map<AttendanceDto>(attendance);
        }

       
        public async Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto)
        {
            
            var existing = await _uow.Attendances.GetByEmployeeAndDateAsync(dto.EmployeeId, dto.Date);
            if (existing != null)
                throw new InvalidOperationException("يوجد سجل حضور لهذا الموظف في هذا اليوم بالفعل");

            var attendance = _mapper.Map<Attendance>(dto);

           
            await CalculateAttendanceAsync(attendance);

            await _uow.Attendances.CreateAsync(attendance);
            await _uow.SaveChangesAsync();

          
            var saved = await _uow.Attendances.GetByIdAsync(attendance.Id);
            return _mapper.Map<AttendanceDto>(saved!);
        }

   
        public async Task<AttendanceDto> UpdateAsync(int id, UpdateAttendanceDto dto)
        {
            var attendance = await _uow.Attendances.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("سجل الحضور غير موجود");

            attendance.CheckInTime = dto.CheckInTime;
            attendance.CheckOutTime = dto.CheckOutTime;

            
            await CalculateAttendanceAsync(attendance);

            await _uow.Attendances.UpdateAsync(attendance);
            await _uow.SaveChangesAsync();

            var updated = await _uow.Attendances.GetByIdAsync(id);
            return _mapper.Map<AttendanceDto>(updated!);
        }

  
        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _uow.Attendances.ExistsAsync(id))
                throw new KeyNotFoundException("سجل الحضور غير موجود");

            await _uow.Attendances.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }

        // ── Import from Excel ─────────────────────────────
        public async Task<ImportResultDto> ImportFromExcelAsync(IFormFile file)
        {
            var result = new ImportResultDto();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

            var toInsert = new List<Attendance>();

            foreach (var row in rows)
            {
                result.TotalRows++;
                try
                {
                    var employeeId = row.Cell(1).GetValue<int>();
                    var date = row.Cell(2).GetDateTime();
                    var checkInStr = row.Cell(3).GetString();
                    var checkOutStr = row.Cell(4).GetString();

                    TimeSpan? checkIn = string.IsNullOrEmpty(checkInStr) ? null : TimeSpan.Parse(checkInStr);
                    TimeSpan? checkOut = string.IsNullOrEmpty(checkOutStr) ? null : TimeSpan.Parse(checkOutStr);

                   
                    var existing = await _uow.Attendances.GetByEmployeeAndDateAsync(employeeId, date);
                    if (existing != null)
                    {
                        result.Errors.Add($"Row {result.TotalRows}: الموظف {employeeId} لديه سجل في {date:yyyy-MM-dd} بالفعل");
                        result.FailedCount++;
                        continue;
                    }

                    var attendance = new Attendance
                    {
                        EmployeeId = employeeId,
                        Date = date,
                        CheckInTime = checkIn,
                        CheckOutTime = checkOut
                    };

                    await CalculateAttendanceAsync(attendance);
                    toInsert.Add(attendance);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {result.TotalRows}: {ex.Message}");
                    result.FailedCount++;
                }
            }

            if (toInsert.Any())
            {
                await _uow.Attendances.BulkCreateAsync(toInsert);
                await _uow.SaveChangesAsync();
            }

            return result;
        }

       
        private async Task CalculateAttendanceAsync(Attendance attendance)
        {
            var settings = await _uow.GeneralSettings.GetAsync();
            var employee = await _uow.Employees.GetByIdAsync(attendance.EmployeeId);

            if (settings == null || employee == null) return;

            var isOfficialHoliday = await _uow.OfficialHolidays
                .IsHolidayAsync(attendance.Date);

            var dayName = attendance.Date.DayOfWeek.ToString();
            var isWeekend = dayName == settings.WeeklyHolidayDay1 ||
                               dayName == settings.WeeklyHolidayDay2;

            if (isOfficialHoliday)
            {
                attendance.Status = AttendanceStatus.Holiday;
                attendance.OvertimeHours = 0;
                attendance.DeductionHours = 0;
                return;
            }

            if (isWeekend)
            {
                attendance.Status = AttendanceStatus.Weekend;
                attendance.OvertimeHours = 0;
                attendance.DeductionHours = 0;
                return;
            }

         
            if (attendance.CheckInTime == null)
            {
                attendance.Status = AttendanceStatus.Absent;
                attendance.OvertimeHours = 0;
                attendance.DeductionHours = 0;
                return;
            }

            // موظف حاضر — احسب الـ Overtime و Deduction
            attendance.Status = AttendanceStatus.Present;

            var scheduledCheckIn = employee.CheckInTime;
            var scheduledCheckOut = employee.CheckOutTime;
            var actualCheckIn = attendance.CheckInTime!.Value;
            var actualCheckOut = attendance.CheckOutTime;

            // حساب التأخير (Deduction)
            if (actualCheckIn > scheduledCheckIn)
            {
                var lateMinutes = (actualCheckIn - scheduledCheckIn).TotalHours;
                attendance.DeductionHours = (decimal)Math.Round(lateMinutes, 2);
            }
            else
            {
                attendance.DeductionHours = 0;
            }

            // حساب الإضافي (Overtime)
            if (actualCheckOut.HasValue && actualCheckOut.Value > scheduledCheckOut)
            {
                var extraHours = (actualCheckOut.Value - scheduledCheckOut).TotalHours;
                attendance.OvertimeHours = (decimal)Math.Round(extraHours, 2);
            }
            else
            {
                attendance.OvertimeHours = 0;
            }
        }
    }
}
