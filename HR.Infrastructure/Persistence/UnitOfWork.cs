using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application;
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
        public UnitOfWork(HrDbContext context)
        {
            _context = context;

            Employees = new EmployeeRepository(_context);
            GeneralSettings = new GeneralSettingsRepository(_context);
            OfficialHolidays = new OfficialHolidayRepository(_context);
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
