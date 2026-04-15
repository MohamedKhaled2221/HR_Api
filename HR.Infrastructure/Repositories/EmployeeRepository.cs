using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using HR.Domain.Repositories;
using HR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using static HR.Infrastructure.Repositories.EmployeeRepository;

namespace HR.Infrastructure.Repositories
{

    #region Employee Repository
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrDbContext _context;

        public EmployeeRepository(HrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            employee.CreatedAt = DateTime.UtcNow;
            employee.UpdatedAt = DateTime.UtcNow;

            await _context.Employees.AddAsync(employee);
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            employee.UpdatedAt = DateTime.UtcNow;

            _context.Employees.Update(employee);

            return await Task.FromResult(employee);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            // Soft Delete
            employee.IsActive = false;
            employee.UpdatedAt = DateTime.UtcNow;


            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Employees
                .AnyAsync(e => e.Id == id && e.IsActive);
        }
    } 
    #endregion
}


