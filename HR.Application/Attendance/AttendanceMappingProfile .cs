using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Domain.Entities;

namespace HR.Application.Attendance
{
    public class AttendanceMappingProfile : Profile
    {
        public AttendanceMappingProfile()
        {
            CreateMap<CreateAttendanceDto,Domain.Entities.Attendance>();
            CreateMap<ImportAttendanceDto,Domain.Entities.Attendance>();

            CreateMap<HR.Domain.Entities.Attendance, AttendanceDto>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => MapStatus(src.Status)));
        }

        private static string MapStatus(AttendanceStatus status) => status switch
        {
            AttendanceStatus.Present => "حاضر",
            AttendanceStatus.Absent => "غائب",
            AttendanceStatus.Holiday => "إجازة رسمية",
            AttendanceStatus.Weekend => "إجازة أسبوعية",
            _ => "غير معروف"
        };
    }
}
