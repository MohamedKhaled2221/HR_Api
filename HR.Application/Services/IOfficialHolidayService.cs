using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.OfficialHoliday.Dtos;

namespace HR.Application.Services
{
    public interface IOfficialHolidayService
    {
        Task<IEnumerable<OfficialHolidayDto>> GetAllAsync(int? year = null);
        Task<OfficialHolidayDto?> GetByIdAsync(int id);
        Task<OfficialHolidayDto> CreateAsync(CreateOfficialHolidayDto dto);
        Task<OfficialHolidayDto> UpdateAsync(int id, UpdateOfficialHolidayDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
