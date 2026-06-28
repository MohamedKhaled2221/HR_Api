using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Identity
{
    public class UserGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PermissionItemDto> Permissions { get; set; } = new();
        public int UsersCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
