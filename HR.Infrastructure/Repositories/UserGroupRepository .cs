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
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly HrDbContext _context;

        public UserGroupRepository(HrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserGroup>> GetAllAsync()
        {
            return await _context.UserGroups
                .Include(g => g.Permissions)
                .Include(g => g.Users.Where(u => u.IsActive))
                .Where(g => g.IsActive)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<UserGroup?> GetByIdAsync(int id)
        {
            return await _context.UserGroups
                .Include(g => g.Permissions)
                .Include(g => g.Users.Where(u => u.IsActive))
                .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);
        }

        public async Task<UserGroup> CreateAsync(UserGroup group)
        {
            group.CreatedAt = DateTime.UtcNow;
            group.UpdatedAt = DateTime.UtcNow;
            await _context.UserGroups.AddAsync(group);
            return group;
        }

        public async Task<UserGroup> UpdateAsync(UserGroup group)
        {
            group.UpdatedAt = DateTime.UtcNow;
            _context.UserGroups.Update(group);
            return await Task.FromResult(group);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.UserGroups.FindAsync(id);
            if (group == null) return false;

            group.IsActive = false;
            group.UpdatedAt = DateTime.UtcNow;
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.UserGroups.AnyAsync(g => g.Id == id && g.IsActive);
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            return await _context.UserGroups.AnyAsync(g =>
                g.Name == name &&
                g.IsActive &&
                (!excludeId.HasValue || g.Id != excludeId.Value));
        }
    }
}
