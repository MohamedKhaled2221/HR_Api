using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Identity;

namespace HR.Application.Interfaces
{
    // ── User Group ────────────────────────────────────────
    public interface IUserGroupRepository
    {
        Task<IEnumerable<UserGroup>> GetAllAsync();
        Task<UserGroup?> GetByIdAsync(int id);
        Task<UserGroup> CreateAsync(UserGroup group);
        Task<UserGroup> UpdateAsync(UserGroup group);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }

}
