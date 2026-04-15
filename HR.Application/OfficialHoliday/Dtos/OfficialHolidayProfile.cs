using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Domain.Entities;


namespace HR.Application.OfficialHoliday.Dtos
{
    public class OfficialHolidayProfile : Profile
    {
        public OfficialHolidayProfile()
        {
            CreateMap<CreateOfficialHolidayDto, Officialholiday>();
            CreateMap<UpdateOfficialHolidayDto, Officialholiday>();
            CreateMap<Officialholiday, OfficialHolidayDto>();
        }
    }
}
