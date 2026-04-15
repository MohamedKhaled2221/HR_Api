using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Domain.Entities; 

namespace HR.Application.GeneralSettings.Dtos
{
    public class GeneralSettingsProfile : Profile
    {
        public GeneralSettingsProfile()
        {
            CreateMap<UpdateGeneralSettingsDto, GeneralsSettings>();
            CreateMap<GeneralsSettings, GeneralSettingsDto>();
        }
    }
}

