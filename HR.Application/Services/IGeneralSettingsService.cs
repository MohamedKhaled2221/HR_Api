using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.GeneralSettings.Dtos;

namespace HR.Application.Services
{
    public interface IGeneralSettingsService
    {
        Task<GeneralSettingsDto?> GetAsync();
        Task<GeneralSettingsDto> UpdateAsync(UpdateGeneralSettingsDto dto);

    }
}
