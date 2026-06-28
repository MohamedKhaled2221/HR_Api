using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    #region OfficialHoliday Seeding 


    public static class OfficialHolidaySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Officialholiday>().HasData(
                new Officialholiday
                {
                    Id = 1,
                    Name = "رأس السنة الميلادية",
                    Date = new DateTime(2021, 1, 1),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 2,
                    Name = "عيد الشرطة",
                    Date = new DateTime(2021, 1, 25),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 3,
                    Name = "ثورة 25 يناير",
                    Date = new DateTime(2021, 1, 25),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 4,
                    Name = "شم النسيم",
                    Date = new DateTime(2021, 5, 3),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 5,
                    Name = "عيد تحرير سيناء",
                    Date = new DateTime(2021, 4, 25),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 6,
                    Name = "نصر 6 أكتوبر",
                    Date = new DateTime(2021, 10, 6),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 7,
                    Name = "ثورة 23 يوليو",
                    Date = new DateTime(2021, 7, 23),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Officialholiday
                {
                    Id = 8,
                    Name = "المولد النبوى الشريف",
                    Date = new DateTime(2021, 10, 21),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                }
            );
        } 
        #endregion
    }
}
    
