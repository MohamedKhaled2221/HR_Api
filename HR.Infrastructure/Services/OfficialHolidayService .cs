using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Application;
using HR.Application.OfficialHoliday.Dtos;
using HR.Application.Services;
using HR.Domain.Entities;

namespace HR.Infrastructure.Services
{
    #region OfficialHoliday Service
    public class OfficialHolidayService : IOfficialHolidayService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OfficialHolidayService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<OfficialHolidayDto>> GetAllAsync(int? year = null)
        {
            var holidays = await _uow.OfficialHolidays.GetAllAsync(year);
            return _mapper.Map<IEnumerable<OfficialHolidayDto>>(holidays);
        }

        public async Task<OfficialHolidayDto?> GetByIdAsync(int id)
        {
            var holiday = await _uow.OfficialHolidays.GetByIdAsync(id);
            return holiday == null ? null : _mapper.Map<OfficialHolidayDto>(holiday);
        }

   
        public async Task<OfficialHolidayDto> CreateAsync(CreateOfficialHolidayDto dto)
        {

            var isDuplicate = await _uow.OfficialHolidays.IsDateDuplicateAsync(dto.Date);
            if (isDuplicate)
                throw new InvalidOperationException("هذا التاريخ موجود بالفعل كإجازة رسمية");

            var holiday = _mapper.Map<Officialholiday>(dto);
            await _uow.OfficialHolidays.CreateAsync(holiday);
            await _uow.SaveChangesAsync();
            return _mapper.Map<OfficialHolidayDto>(holiday);
        }

      
        public async Task<OfficialHolidayDto> UpdateAsync(int id, UpdateOfficialHolidayDto dto)
        {
            var holiday = await _uow.OfficialHolidays.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("الإجازة غير موجودة");

           
            var isDuplicate = await _uow.OfficialHolidays.IsDateDuplicateAsync(dto.Date, excludeId: id);
            if (isDuplicate)
                throw new InvalidOperationException("هذا التاريخ موجود بالفعل كإجازة رسمية");

            _mapper.Map(dto, holiday);
            await _uow.OfficialHolidays.UpdateAsync(holiday);
            await _uow.SaveChangesAsync();
            return _mapper.Map<OfficialHolidayDto>(holiday);
        }

     
        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _uow.OfficialHolidays.ExistsAsync(id))
                throw new KeyNotFoundException("الإجازة غير موجودة");

            await _uow.OfficialHolidays.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }
    } 
    #endregion
}