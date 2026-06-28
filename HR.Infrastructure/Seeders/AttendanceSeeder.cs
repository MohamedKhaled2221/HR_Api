using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    public static class AttendanceSeeder
    {
     
        private static bool IsWeekend(DateTime date)
            => date.DayOfWeek == DayOfWeek.Friday ||
               date.DayOfWeek == DayOfWeek.Saturday;

      
        private static readonly List<DateTime> OfficialHolidays = new()
        {
       
            new DateTime(2021, 10, 21)
        };

        private static bool IsOfficialHoliday(DateTime date)
            => OfficialHolidays.Any(h => h.Date == date.Date);

        public static void Seed(ModelBuilder modelBuilder)
        {
            var records = new List<Attendance>();
            int id = 1;

            
            var employeeSchedules = new Dictionary<int, (TimeSpan In, TimeSpan Out, decimal Salary)>
            {
                { 1, (new TimeSpan(9, 0, 0),  new TimeSpan(17, 0, 0), 8000m)  },
                { 2, (new TimeSpan(9, 0, 0),  new TimeSpan(17, 0, 0), 6500m)  },
                { 3, (new TimeSpan(8, 30, 0), new TimeSpan(16, 30, 0), 12000m) },
                { 4, (new TimeSpan(9, 0, 0),  new TimeSpan(17, 0, 0), 5000m)  },
                { 5, (new TimeSpan(9, 0, 0),  new TimeSpan(17, 0, 0), 9500m)  },
                { 6, (new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0), 4500m)  },
                { 7, (new TimeSpan(8, 0, 0),  new TimeSpan(16, 0, 0), 15000m) },
                { 8, (new TimeSpan(9, 0, 0),  new TimeSpan(17, 0, 0), 7000m)  },
            };

          
            foreach (var (employeeId, schedule) in employeeSchedules)
            {
                for (int day = 1; day <= 30; day++)
                {
                    var date = new DateTime(2021, 11, day);

                  
                    if (IsWeekend(date))
                    {
                        records.Add(new Attendance
                        {
                            Id = id++,
                            EmployeeId = employeeId,
                            Date = date,
                            CheckInTime = null,
                            CheckOutTime = null,
                            OvertimeHours = 0,
                            DeductionHours = 0,
                            Status = AttendanceStatus.Weekend,
                            IsActive = true,
                            CreatedAt = new DateTime(2021, 11, 8),
                            UpdatedAt = new DateTime(2021, 11, 8)
                        });
                        continue;
                    }

               
                    if (IsOfficialHoliday(date))
                    {
                        records.Add(new Attendance
                        {
                            Id = id++,
                            EmployeeId = employeeId,
                            Date = date,
                            CheckInTime = null,
                            CheckOutTime = null,
                            OvertimeHours = 0,
                            DeductionHours = 0,
                            Status = AttendanceStatus.Holiday,
                            IsActive = true,
                            CreatedAt = new DateTime(2021, 11, 8),
                            UpdatedAt = new DateTime(2021, 11, 8)
                        });
                        continue;
                    }

                    
                    var scenario = GetScenario(employeeId, day);

                    TimeSpan? checkIn;
                    TimeSpan? checkOut;
                    decimal overtime;
                    decimal deduction;
                    AttendanceStatus status;

                    switch (scenario)
                    {
                     
                        case "absent":
                            checkIn = null;
                            checkOut = null;
                            overtime = 0;
                            deduction = 0;
                            status = AttendanceStatus.Absent;
                            break;

                     
                        case "late":
                            checkIn = schedule.In.Add(TimeSpan.FromHours(1));
                            checkOut = schedule.Out;
                            overtime = 0;
                            deduction = 1;
                            status = AttendanceStatus.Present;
                            break;

                       
                        case "overtime":
                            checkIn = schedule.In;
                            checkOut = schedule.Out.Add(TimeSpan.FromHours(2));
                            overtime = 2;
                            deduction = 0;
                            status = AttendanceStatus.Present;
                            break;

                        case "late_overtime":
                            checkIn = schedule.In.Add(TimeSpan.FromHours(1));
                            checkOut = schedule.Out.Add(TimeSpan.FromHours(2));
                            overtime = 2;
                            deduction = 1;
                            status = AttendanceStatus.Present;
                            break;

                        
                        default:
                            checkIn = schedule.In;
                            checkOut = schedule.Out;
                            overtime = 0;
                            deduction = 0;
                            status = AttendanceStatus.Present;
                            break;
                    }

                    records.Add(new Attendance
                    {
                        Id = id++,
                        EmployeeId = employeeId,
                        Date = date,
                        CheckInTime = checkIn,
                        CheckOutTime = checkOut,
                        OvertimeHours = overtime,
                        DeductionHours = deduction,
                        Status = status,
                        IsActive = true,
                        CreatedAt = new DateTime(2021, 11, 8),
                        UpdatedAt = new DateTime(2021, 11, 8)
                    });
                }
            }

            modelBuilder.Entity<Attendance>().HasData(records);
        }

        
        // normal / late / absent / overtime / late_overtime
        private static string GetScenario(int employeeId, int day)
        {
            return (employeeId, day) switch
            {
             
                (1, 3) => "late",
                (1, 10) => "late",
                (1, 17) => "overtime",

              
                (2, 5) => "absent",
                (2, 14) => "late",
                (2, 22) => "late",

              
                (3, 2) => "overtime",
                (3, 9) => "overtime",
                (3, 16) => "overtime",
                (3, 23) => "overtime",

               
                (4, 7) => "absent",
                (4, 18) => "absent",
                (4, 25) => "late",

               
                (5, 4) => "late_overtime",
                (5, 11) => "late_overtime",
                (5, 28) => "late",

             
                (6, 8) => "absent",
                (6, 15) => "overtime",
                (6, 29) => "late",

               
                (7, 1) => "overtime",
                (7, 30) => "overtime",

              
                (8, 6) => "late",
                (8, 13) => "absent",
                (8, 20) => "late_overtime",

              
                _ => "normal"
            };
        }
    }
}
