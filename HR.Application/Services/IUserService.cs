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
    #region User Service
    public interface IUserService
    {
        Task<IEnumerable<AppUserDto>> GetAllAsync();
        Task<AppUserDto?> GetByIdAsync(int id);
        Task<AppUserDto> CreateAsync(CreateUserDto dto);
        Task<AppUserDto> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task ChangePasswordAsync(int id, ChangePasswordDto dto);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<AppUserDto>>(users);
        }

        public async Task<AppUserDto?> GetByIdAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<AppUserDto>(user);
        }

        public async Task<AppUserDto> CreateAsync(CreateUserDto dto)
        {
            // تحقق من تكرار اسم المستخدم
            if (await _uow.Users.UsernameExistsAsync(dto.Username))
                throw new InvalidOperationException("اسم المستخدم موجود بالفعل");

            // تحقق من تكرار الايميل
            if (await _uow.Users.EmailExistsAsync(dto.Email))
                throw new InvalidOperationException("البريد الالكتروني موجود بالفعل");

            // تحقق من وجود المجموعة
            if (!await _uow.UserGroups.ExistsAsync(dto.UserGroupId))
                throw new KeyNotFoundException("المجموعة غير موجودة");

            var user = _mapper.Map<AppUser>(dto);

            // Hash Password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();

            var created = await _uow.Users.GetByIdAsync(user.Id);
            return _mapper.Map<AppUserDto>(created!);
        }

        public async Task<AppUserDto> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("المستخدم غير موجود");

            // تحقق تكرار Username مع استثناء الحالي
            if (await _uow.Users.UsernameExistsAsync(dto.Username, excludeId: id))
                throw new InvalidOperationException("اسم المستخدم موجود بالفعل");

            // تحقق تكرار Email مع استثناء الحالي
            if (await _uow.Users.EmailExistsAsync(dto.Email, excludeId: id))
                throw new InvalidOperationException("البريد الالكتروني موجود بالفعل");

            // تحقق من وجود المجموعة
            if (!await _uow.UserGroups.ExistsAsync(dto.UserGroupId))
                throw new KeyNotFoundException("المجموعة غير موجودة");

            _mapper.Map(dto, user);

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();

            var updated = await _uow.Users.GetByIdAsync(id);
            return _mapper.Map<AppUserDto>(updated!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _uow.Users.ExistsAsync(id))
                throw new KeyNotFoundException("المستخدم غير موجود");

            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task ChangePasswordAsync(int id, ChangePasswordDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("المستخدم غير موجود");

            // تحقق من الباسورد الحالي
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("كلمة المرور الحالية غير صحيحة");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
        }
    } 
    #endregion
}
