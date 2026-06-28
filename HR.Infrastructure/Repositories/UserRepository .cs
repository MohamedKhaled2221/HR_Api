using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Application.Interfaces;
using HR.Domain.Identity;
using HR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Repositories
{
    #region User Repository
    public class UserRepository : IUserRepository
    {
        private readonly HrDbContext _context;

        public UserRepository(HrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserGroup)
                .Where(u => u.IsActive)
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserGroup)
                    .ThenInclude(g => g.Permissions)
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
        }

        public async Task<AppUser?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users
                .Include(u => u.UserGroup)
                    .ThenInclude(g => g.Permissions)
                .FirstOrDefaultAsync(u =>
                    (u.Username == usernameOrEmail || u.Email == usernameOrEmail)
                    && u.IsActive);
        }

        public async Task<AppUser> CreateAsync(AppUser user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task<AppUser> UpdateAsync(AppUser user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            return await Task.FromResult(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && u.IsActive);
        }

        public async Task<bool> UsernameExistsAsync(string username, int? excludeId = null)
        {
            return await _context.Users.AnyAsync(u =>
                u.Username == username &&
                u.IsActive &&
                (!excludeId.HasValue || u.Id != excludeId.Value));
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Users.AnyAsync(u =>
                u.Email == email &&
                u.IsActive &&
                (!excludeId.HasValue || u.Id != excludeId.Value));
        }
    } 
    #endregion
}
