using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
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
            });


            EmployeeSeeder.Seed(modelBuilder);
            GeneralSettingsSeeder.Seed(modelBuilder);
            OfficialHolidaySeeder.Seed(modelBuilder);
        }
    } 
    #endregion
}