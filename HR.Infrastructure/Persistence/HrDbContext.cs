using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using HR.Domain.Identity;
using HR.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Persistence
{
    #region DbContext

    public class HrDbContext : DbContext
    {
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<GeneralsSettings> GeneralSettings { get; set; }
        public DbSet<Officialholiday> Officialholidays { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(14);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BasicSalary)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                modelBuilder.Entity<GeneralsSettings>()
                .Property(x => x.DeductionHourValue)
                .HasPrecision(10, 2);

                modelBuilder.Entity<GeneralsSettings>()
                    .Property(x => x.OvertimeHourValue)
                    .HasPrecision(10, 2);
                // ── Attendance Configuration ───────────────
                modelBuilder.Entity<Attendance>(entity =>
                {
                    entity.HasKey(a => a.Id);

                    entity.Property(a => a.OvertimeHours)
                        .HasColumnType("decimal(18,2)");

                    entity.Property(a => a.DeductionHours)
                        .HasColumnType("decimal(18,2)");

                    entity.Property(a => a.Status)
                        .HasConversion<int>();

                    entity.Property(a => a.CreatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");

                    entity.Property(a => a.UpdatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");

                 
                    entity.HasOne(a => a.Employee)
                  .WithMany(e => e.Attendances)
                  .HasForeignKey(a => a.EmployeeId)
                  .OnDelete(DeleteBehavior.Restrict);

               
                    entity.HasIndex(a => new { a.EmployeeId, a.Date })
                        .IsUnique()
                        .HasFilter("[IsActive] = 1");

                  
                    modelBuilder.Entity<UserGroup>(entity =>
                    {
                        entity.HasKey(g => g.Id);
                        entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                        entity.HasIndex(g => g.Name).IsUnique();
                        entity.HasMany(g => g.Permissions).WithOne(p => p.UserGroup)
                            .HasForeignKey(p => p.UserGroupId).OnDelete(DeleteBehavior.Cascade);
                        entity.HasMany(g => g.Users).WithOne(u => u.UserGroup)
                            .HasForeignKey(u => u.UserGroupId).OnDelete(DeleteBehavior.Restrict);
                    });

                   
                    modelBuilder.Entity<GroupPermission>(entity =>
                    {
                        entity.HasKey(p => p.Id);
                        entity.Property(p => p.Screen).HasConversion<int>();
                        entity.Property(p => p.Action).HasConversion<int>();
                        entity.HasIndex(p => new { p.UserGroupId, p.Screen, p.Action }).IsUnique();
                    });

                   
                    modelBuilder.Entity<AppUser>(entity =>
                    {
                        entity.HasKey(u => u.Id);
                        entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                        entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                        entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                        entity.Property(u => u.PasswordHash).IsRequired();
                        entity.HasIndex(u => u.Username).IsUnique();
                        entity.HasIndex(u => u.Email).IsUnique();
                    });
                });
            });


            EmployeeSeeder.Seed(modelBuilder);
            GeneralSettingsSeeder.Seed(modelBuilder);
            OfficialHolidaySeeder.Seed(modelBuilder);
            AttendanceSeeder.Seed(modelBuilder);
            AuthSeeder.Seed(modelBuilder);
        }
    } 
    #endregion
}