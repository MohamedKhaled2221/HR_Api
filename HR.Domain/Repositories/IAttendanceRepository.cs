using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.Attendance;
using HR.Domain.Entities;

namespace HR.Domain.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync(AttendanceFilterDto filter);
        Task<Attendance?> GetByIdAsync(int id);
        Task<Attendance?> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<Attendance> CreateAsync(Attendance attendance);
        Task<Attendance> UpdateAsync(Attendance attendance);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task BulkCreateAsync(IEnumerable<Attendance> attendances);
    }
}
