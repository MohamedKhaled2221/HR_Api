using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using HR.Application;
using HR.Application.Interfaces;
using HR.Domain.Identity;
using HR.Domain.Repositories;
using HR.Infrastructure.Repositories;

namespace HR.Infrastructure.Persistence
{
    #region UnitOfWork

    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrDbContext _context;


        public IEmployeeRepository Employees { get; }
        public IGeneralSettingsRepository GeneralSettings { get; }
        public IOfficialHolidayRepository OfficialHolidays { get; }
        public IAttendanceRepository Attendances { get; }
        public ISalaryRepository Salary { get; }
        public IUserGroupRepository UserGroups { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(HrDbContext context)
        {
            _context = context;

            Employees = new EmployeeRepository(_context);
            GeneralSettings = new GeneralSettingsRepository(_context);
            OfficialHolidays = new OfficialHolidayRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Salary = new SalaryRepository(_context);
            UserGroups = new UserGroupRepository(_context);
            Users = new UserRepository(_context);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    } 
    #endregion
}
