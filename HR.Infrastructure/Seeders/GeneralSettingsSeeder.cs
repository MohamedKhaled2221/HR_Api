using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    #region GeneralSettings Seeding
    public static class GeneralSettingsSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralsSettings>().HasData(
                new GeneralsSettings
                {
                    Id = 1,
                    OvertimeHourValue = 2,
                    DeductionHourValue = 2,
                    WeeklyHolidayDay1 = "الجمعة",
                    WeeklyHolidayDay2 = "السبت",
                    UpdatedAt = new DateTime(2021, 11, 8)
                }
            );
        } 
        #endregion
    }
}
