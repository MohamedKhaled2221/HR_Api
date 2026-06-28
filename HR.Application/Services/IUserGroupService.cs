using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Application.Identity;
using HR.Domain.Identity;

namespace HR.Application.Services
{
    public interface IUserGroupService
    {
        Task<IEnumerable<UserGroupDto>> GetAllAsync();
        Task<UserGroupDto?> GetByIdAsync(int id);
        Task<UserGroupDto> CreateAsync(CreateUserGroupDto dto);
        Task<UserGroupDto> UpdateAsync(int id, UpdateUserGroupDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class UserGroupService : IUserGroupService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserGroupService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserGroupDto>> GetAllAsync()
        {
            var groups = await _uow.UserGroups.GetAllAsync();
            return _mapper.Map<IEnumerable<UserGroupDto>>(groups);
        }

        public async Task<UserGroupDto?> GetByIdAsync(int id)
        {
            var group = await _uow.UserGroups.GetByIdAsync(id);
            return group == null ? null : _mapper.Map<UserGroupDto>(group);
        }

        public async Task<UserGroupDto> CreateAsync(CreateUserGroupDto dto)
        {
            // منع تكرار الاسم
            if (await _uow.UserGroups.NameExistsAsync(dto.Name))
                throw new InvalidOperationException("اسم المجموعة موجود بالفعل");

            var group = _mapper.Map<UserGroup>(dto);

            // أضف الصلاحيات
            group.Permissions = dto.Permissions
                .Select(p => new GroupPermission
                {
                    Screen = (PermissionScreen)p.Screen,
                    Action = (PermissionAction)p.Action
                }).ToList();

            await _uow.UserGroups.CreateAsync(group);
            await _uow.SaveChangesAsync();

            var created = await _uow.UserGroups.GetByIdAsync(group.Id);
            return _mapper.Map<UserGroupDto>(created!);
        }

        public async Task<UserGroupDto> UpdateAsync(int id, UpdateUserGroupDto dto)
        {
            var group = await _uow.UserGroups.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("المجموعة غير موجودة");

            // منع تكرار الاسم مع استثناء الحالي
            if (await _uow.UserGroups.NameExistsAsync(dto.Name, excludeId: id))
                throw new InvalidOperationException("اسم المجموعة موجود بالفعل");

            group.Name = dto.Name;
            group.UpdatedAt = DateTime.UtcNow;

            // حذف الصلاحيات القديمة و إضافة الجديدة
            group.Permissions.Clear();
            foreach (var p in dto.Permissions)
            {
                group.Permissions.Add(new GroupPermission
                {
                    UserGroupId = id,
                    Screen = (PermissionScreen)p.Screen,
                    Action = (PermissionAction)p.Action
                });
            }

            await _uow.UserGroups.UpdateAsync(group);
            await _uow.SaveChangesAsync();

            var updated = await _uow.UserGroups.GetByIdAsync(id);
            return _mapper.Map<UserGroupDto>(updated!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _uow.UserGroups.ExistsAsync(id))
                throw new KeyNotFoundException("المجموعة غير موجودة");

            await _uow.UserGroups.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
