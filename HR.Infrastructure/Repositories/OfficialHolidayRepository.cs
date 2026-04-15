using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using HR.Domain.Repositories;
using HR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    #region OfficialHoliday Repository
    public class OfficialHolidayRepository : IOfficialHolidayRepository
    {
        private readonly HrDbContext _context;

        public OfficialHolidayRepository(HrDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Officialholiday>> GetAllAsync(int? year = null)
        {
            var query = _context.Officialholidays
                .Where(h => h.IsActive);

            if (year.HasValue)
                query = query.Where(h => h.Date.Year == year.Value);

            return await query
                .OrderBy(h => h.Date)
                .ToListAsync();
        }

        public async Task<Officialholiday?> GetByIdAsync(int id)
        {
            return await _context.Officialholidays
                .FirstOrDefaultAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<Officialholiday> CreateAsync(Officialholiday holiday)
        {
            holiday.CreatedAt = DateTime.UtcNow;
            holiday.UpdatedAt = DateTime.UtcNow;

            await _context.Officialholidays.AddAsync(holiday);
            return holiday;

        }

        public async Task<Officialholiday> UpdateAsync(Officialholiday holiday)
        {
            holiday.UpdatedAt = DateTime.UtcNow;
            _context.Officialholidays.Update(holiday);
            return await Task.FromResult(holiday);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var holiday = await _context.Officialholidays.FindAsync(id);
            if (holiday == null) return false;

            // Soft Delete
            holiday.IsActive = false;
            holiday.UpdatedAt = DateTime.UtcNow;
            return true;

        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Officialholidays
                .AnyAsync(h => h.Id == id && h.IsActive);
        }


        public async Task<bool> IsDateDuplicateAsync(DateTime date, int? excludeId = null)
        {
            return await _context.Officialholidays
                .AnyAsync(h => h.Date.Date == date.Date
                            && h.IsActive
                            && (!excludeId.HasValue || h.Id != excludeId.Value));
        }
    } 
    #endregion
}
