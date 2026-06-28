using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Domain.Identity;

namespace HR.Application.Identity
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
         
            CreateMap<CreateUserGroupDto, UserGroup>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());

            CreateMap<UpdateUserGroupDto, UserGroup>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());

            CreateMap<UserGroup, UserGroupDto>()
                .ForMember(dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.Permissions.Select(p => new PermissionItemDto
                    {
                        Screen = (int)p.Screen,
                        Action = (int)p.Action
                    }).ToList()))
                .ForMember(dest => dest.UsersCount,
                    opt => opt.MapFrom(src => src.Users.Count(u => u.IsActive)));

            CreateMap<PermissionItemDto, GroupPermission>()
                .ForMember(dest => dest.Screen, opt => opt.MapFrom(src => (PermissionScreen)src.Screen))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => (PermissionAction)src.Action));

            CreateMap<CreateUserDto, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UpdateUserDto, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<AppUser, AppUserDto>()
                .ForMember(dest => dest.GroupName,
                    opt => opt.MapFrom(src => src.UserGroup != null ? src.UserGroup.Name : string.Empty));
        }
    }
}
