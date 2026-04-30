using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.Identity;
using HR.Infrastructure.Services;

namespace HR.Application.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwt;

        public AuthService(IUnitOfWork uow, IJwtService jwt)
        {
            _uow = uow;
            _jwt = jwt;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
           
            var user = await _uow.Users.GetByUsernameOrEmailAsync(dto.UsernameOrEmail)
                ?? throw new UnauthorizedAccessException("من فضلك ادخل اسم مستخدم صالح");

            // check password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("من فضلك ادخل كلمة مرور صالحة");

            
            var token = _jwt.GenerateToken(user);

            
            var permissions = user.UserGroup?.Permissions
                .Select(p => $"{(int)p.Screen}:{(int)p.Action}")
                .ToList() ?? new();

            return new LoginResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Email = user.Email,
                GroupName = user.UserGroup?.Name ?? string.Empty,
                Permissions = permissions
            };
        }
    }
}
