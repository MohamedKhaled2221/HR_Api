using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.Attendance;
using HR.Domain.Entities;
using HR.Domain.Repositories;
using HR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrDbContext _context;

        public AttendanceRepository(HrDbContext context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Attendance>> GetAllAsync(AttendanceFilterDto filter)
        {
            var query = _context.Attendances
                .Include(a => a.Employee)
                .Where(a => a.IsActive);

           
            if (filter.EmployeeId.HasValue)
                query = query.Where(a => a.EmployeeId == filter.EmployeeId.Value);

            if (!string.IsNullOrEmpty(filter.EmployeeName))
                query = query.Where(a => a.Employee.FullName.Contains(filter.EmployeeName));

         
            if (filter.DateFrom.HasValue)
                query = query.Where(a => a.Date >= filter.DateFrom.Value);

          
            if (filter.DateTo.HasValue)
                query = query.Where(a => a.Date <= filter.DateTo.Value);

            return await query
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Employee.FullName)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
        }

     
        public async Task<Attendance?> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId
                                       && a.Date.Date == date.Date
                                       && a.IsActive);
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            attendance.CreatedAt = DateTime.UtcNow;
            attendance.UpdatedAt = DateTime.UtcNow;

            await _context.Attendances.AddAsync(attendance);
            return attendance;
            // SaveChanges هيتعمل في الـ UnitOfWork
        }

        public async Task<Attendance> UpdateAsync(Attendance attendance)
        {
            attendance.UpdatedAt = DateTime.UtcNow;
            _context.Attendances.Update(attendance);
            return await Task.FromResult(attendance);
     
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return false;

            // Soft Delete
            attendance.IsActive = false;
            attendance.UpdatedAt = DateTime.UtcNow;
            return true;
           
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Attendances
                .AnyAsync(a => a.Id == id && a.IsActive);
        }

       
        public async Task BulkCreateAsync(IEnumerable<Attendance> attendances)
        {
            var list = attendances.Select(a =>
            {
                a.CreatedAt = DateTime.UtcNow;
                a.UpdatedAt = DateTime.UtcNow;
                return a;
            });

            await _context.Attendances.AddRangeAsync(list);
          
        }
    }
}
