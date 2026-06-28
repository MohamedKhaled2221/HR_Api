using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;

namespace HR.Domain.Repositories
{
    public interface IOfficialHolidayRepository
    {
        Task<IEnumerable<Officialholiday>> GetAllAsync(int? year = null);
        Task<Officialholiday?> GetByIdAsync(int id);
        Task<Officialholiday> CreateAsync(Officialholiday holiday);
        Task<Officialholiday> UpdateAsync(Officialholiday holiday);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsDateDuplicateAsync(DateTime date, int? excludeId = null);
        Task<bool> IsHolidayAsync(DateTime date);
    }
}
