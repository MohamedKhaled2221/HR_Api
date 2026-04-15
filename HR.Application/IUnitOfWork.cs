using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Repositories;

namespace HR.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IGeneralSettingsRepository GeneralSettings { get; }
        IOfficialHolidayRepository OfficialHolidays { get; }

        Task<int> SaveChangesAsync();
    }
}
