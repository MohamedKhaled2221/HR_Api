using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application;
using HR.Application.SalaryReport.Dtos;
using HR.Application.Services;
using HR.Domain.Entities;

namespace HR.Infrastructure.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _uow;

        public SalaryService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<SalaryReportDto>> GetAllAsync(SalaryFilterDto filter)
        {
            var settings = await _uow.GeneralSettings.GetAsync();
            var employees = await _uow.Salary.GetAllEmployeesWithAttendanceAsync(
                                filter.Month, filter.Year);

           
            if (!string.IsNullOrEmpty(filter.EmployeeName))
                employees = employees.Where(e =>
                    e.FullName.Contains(filter.EmployeeName,
                    StringComparison.OrdinalIgnoreCase));

            return employees.Select(e => CalculateSalary(e, filter.Month, filter.Year, settings!));
        }
        public async Task<SalaryReportDto?> GetByEmployeeAsync(SalaryFilterDto filter)
        {
            if (!filter.EmployeeId.HasValue) return null;

            var settings = await _uow.GeneralSettings.GetAsync();
            var employee = await _uow.Salary.GetEmployeeWithAttendanceAsync(
                               filter.EmployeeId.Value, filter.Month, filter.Year);

            if (employee == null) return null;

            return CalculateSalary(employee, filter.Month, filter.Year, settings!);
        }
        private static SalaryReportDto CalculateSalary(
            Employee employee, int month, int year, GeneralsSettings settings)
        {
            var attendance = employee.Attendances.ToList();

       
            var presentDays = attendance.Count(a => a.Status == AttendanceStatus.Present);
            var absentDays = attendance.Count(a => a.Status == AttendanceStatus.Absent);

            var overtimeHours = attendance.Sum(a => a.OvertimeHours ?? 0);
            var deductionHours = attendance.Sum(a => a.DeductionHours ?? 0);

            var hourlyRate = employee.BasicSalary / (30 * 8);

          
            var totalOvertime = overtimeHours * settings.OvertimeHourValue * hourlyRate;
            var totalDeduction = deductionHours * settings.DeductionHourValue * hourlyRate;
            var dailyRate = employee.BasicSalary / 30;
            var absenceDeduction = absentDays * dailyRate;
            var netSalary = employee.BasicSalary + totalOvertime - totalDeduction - absenceDeduction;

            return new SalaryReportDto
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                BasicSalary = employee.BasicSalary,
                PresentDays = presentDays,
                AbsentDays = absentDays,
                OvertimeHours = overtimeHours,
                DeductionHours = deductionHours,
                TotalOvertime = Math.Round(totalOvertime, 2),
                TotalDeduction = Math.Round(totalDeduction + absenceDeduction, 2),
                NetSalary = Math.Round(netSalary, 2),
                Month = month,
                Year = year
            };
        }
    }
}
