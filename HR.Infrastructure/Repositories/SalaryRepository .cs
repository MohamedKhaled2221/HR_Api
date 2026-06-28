using HR.Domain.Entities;
using HR.Domain.Repositories;
using HR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly HrDbContext _context;

        public SalaryRepository(HrDbContext context)
        {
            _context = context;
        }

     
        public async Task<IEnumerable<Attendance>> GetAttendanceByMonthAsync(
            int employeeId, int month, int year)
        {
            return await _context.Attendances
                .Where(a => a.EmployeeId == employeeId
                         && a.Date.Month == month
                         && a.Date.Year == year
                         && a.IsActive)
                .ToListAsync();
        }

   
        public async Task<IEnumerable<Employee>> GetAllEmployeesWithAttendanceAsync(
            int month, int year)
        {
            return await _context.Employees
                .Include(e => e.Attendances
                    .Where(a => a.Date.Month == month
                             && a.Date.Year == year
                             && a.IsActive))
                .Where(e => e.IsActive)
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }

       
        public async Task<Employee?> GetEmployeeWithAttendanceAsync(
            int employeeId, int month, int year)
        {
            return await _context.Employees
                .Include(e => e.Attendances
                    .Where(a => a.Date.Month == month
                             && a.Date.Year == year
                             && a.IsActive))
                .FirstOrDefaultAsync(e => e.Id == employeeId && e.IsActive);
        }
    }

}
