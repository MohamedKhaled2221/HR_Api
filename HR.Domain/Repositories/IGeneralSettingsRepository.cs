using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;

namespace HR.Domain.Repositories
{
    public interface IGeneralSettingsRepository
    {
        Task<GeneralsSettings?> GetAsync();
        Task<GeneralsSettings> UpdateAsync(GeneralsSettings settings);
    }
}
