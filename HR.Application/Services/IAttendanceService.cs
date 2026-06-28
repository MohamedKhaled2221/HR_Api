using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.Attendance;
using Microsoft.AspNetCore.Http;

namespace HR.Application.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync(AttendanceFilterDto filter);
        Task<AttendanceDto?> GetByIdAsync(int id);
        Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto);
        Task<AttendanceDto> UpdateAsync(int id, UpdateAttendanceDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ImportResultDto> ImportFromExcelAsync(IFormFile file);
    }
}
