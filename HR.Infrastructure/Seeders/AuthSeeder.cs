using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Math;
using HR.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    public static class AuthSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup
                {
                    Id = 1,
                    Name = "Administrators",
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new UserGroup
                {
                    Id = 2,
                    Name = "HR Staff",
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                }
            );

           
            var permId = 1;
            var screens = Enum.GetValues<PermissionScreen>();
            var actions = Enum.GetValues<PermissionAction>();
            var adminPerms = new List<GroupPermission>();

            foreach (var screen in screens)
                foreach (var action in actions)
                    adminPerms.Add(new GroupPermission
                    {
                        Id = permId++,
                        UserGroupId = 1,
                        Screen = screen,
                        Action = action
                    });

          
            var hrScreens = new[]
            {
                PermissionScreen.Employees,
                PermissionScreen.Attendance,
                PermissionScreen.SalaryReport,
                PermissionScreen.OfficialHolidays,
                PermissionScreen.GeneralSettings
            };

            foreach (var screen in hrScreens)
                foreach (var action in actions)
                    adminPerms.Add(new GroupPermission
                    {
                        Id = permId++,
                        UserGroupId = 2,
                        Screen = screen,
                        Action = action
                    });

            modelBuilder.Entity<GroupPermission>().HasData(adminPerms);

          
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    FullName = "System Admin",
                    Username = "admin",
                    Email = "admin@pioneers-solutions.com",
               
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    UserGroupId = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new AppUser
                {
                    Id = 2,
                    FullName = "HR Manager",
                    Username = "hrmanager",
                    Email = "hr@pioneers-solutions.com",
                  
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Hr@12345"),
                    UserGroupId = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                }
            );
        }
    }
}
