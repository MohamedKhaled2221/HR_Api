using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Identity
{
    public enum PermissionScreen
    {
        Employees = 1,
        Attendance = 2,
        SalaryReport = 3,
        OfficialHolidays = 4,
        GeneralSettings = 5,
        Users = 6,
        UserGroups = 7
    }

   
    public enum PermissionAction
    {
        View = 1,
        Add = 2,
        Edit = 3,
        Delete = 4
    }

  
    public class GroupPermission
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public PermissionScreen Screen { get; set; }
        public PermissionAction Action { get; set; }

    
        public UserGroup UserGroup { get; set; } = null!;
    }
}
