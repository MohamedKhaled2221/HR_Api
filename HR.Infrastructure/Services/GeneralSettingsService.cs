using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Application;
using HR.Application.GeneralSettings.Dtos;
using HR.Application.Services;
using HR.Domain.Repositories;

namespace HR.Infrastructure.Services
{
    #region GeneralSettings Service
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GeneralSettingsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

     
        public async Task<GeneralSettingsDto?> GetAsync()
        {
            var settings = await _uow.GeneralSettings.GetAsync();
            return settings == null ? null : _mapper.Map<GeneralSettingsDto>(settings);
        }

    
        public async Task<GeneralSettingsDto> UpdateAsync(UpdateGeneralSettingsDto dto)
        {
            var settings = await _uow.GeneralSettings.GetAsync()
                ?? throw new KeyNotFoundException("لم يتم العثور على الإعدادات");

            _mapper.Map(dto, settings);
            await _uow.GeneralSettings.UpdateAsync(settings);
            await _uow.SaveChangesAsync();
            return _mapper.Map<GeneralSettingsDto>(settings);
        }
    } 
    #endregion
}
