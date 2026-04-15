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
    #region GeneralSettings Repository 
    public class GeneralSettingsRepository : IGeneralSettingsRepository
    {
        private readonly HrDbContext _context;

        public GeneralSettingsRepository(HrDbContext context)
        {
            _context = context;
        }


        public async Task<GeneralsSettings?> GetAsync()
        {
            return await _context.GeneralSettings.FirstOrDefaultAsync();
        }

        public async Task<GeneralsSettings> UpdateAsync(GeneralsSettings settings)
        {
            settings.UpdatedAt = DateTime.UtcNow;
            _context.GeneralSettings.Update(settings);
            return await Task.FromResult(settings);
        }
    } 
    #endregion

}
