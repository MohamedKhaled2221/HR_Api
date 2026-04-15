using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Seeders
{
    #region Employee Seeding
    public static class EmployeeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Mohamed Ahmed Ismail",
                    Address = "Cairo",
                    Phone = "01012345678",
                    BirthDate = new DateTime(1990, 5, 15),
                    Gender = "Male",
                    NationalId = "29005151234567",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2015, 3, 1),
                    BasicSalary = 8000,
                    CheckInTime = new TimeSpan(9, 0, 0),
                    CheckOutTime = new TimeSpan(17, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 2,
                    FullName = "sara Mahmoud",
                    Address = "اGiza, Doki",
                    Phone = "01123456789",
                    BirthDate = new DateTime(1993, 8, 22),
                    Gender = "Female",
                    NationalId = "29308221234568",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2018, 6, 15),
                    BasicSalary = 6500,
                    CheckInTime = new TimeSpan(9, 0, 0),
                    CheckOutTime = new TimeSpan(17, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 3,
                    FullName = "Ahmed Khaled Hassan",
                    Address = "Alexandria",
                    Phone = "01234567890",
                    BirthDate = new DateTime(1988, 1, 10),
                    Gender = "Male",
                    NationalId = "28801101234569",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2012, 9, 1),
                    BasicSalary = 12000,
                    CheckInTime = new TimeSpan(8, 30, 0),
                    CheckOutTime = new TimeSpan(16, 30, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 4,
                    FullName = "Menna Ibrahim",
                    Address = "Cairo",
                    Phone = "01098765432",
                    BirthDate = new DateTime(1995, 3, 30),
                    Gender = "Female",
                    NationalId = "29503301234560",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2020, 1, 5),
                    BasicSalary = 5000,
                    CheckInTime = new TimeSpan(9, 0, 0),
                    CheckOutTime = new TimeSpan(17, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 5,
                    FullName = "Omar Yousef",
                    Address = "October",
                    Phone = "01511223344",
                    BirthDate = new DateTime(1991, 11, 5),
                    Gender = "Male",
                    NationalId = "29111051234561",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2016, 4, 10),
                    BasicSalary = 9500,
                    CheckInTime = new TimeSpan(9, 0, 0),
                    CheckOutTime = new TimeSpan(17, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 6,
                    FullName = "Nourahan Ali",
                    Address = "Maadi",
                    Phone = "01677889900",
                    BirthDate = new DateTime(1997, 6, 18),
                    Gender = "Female",
                    NationalId = "29706181234562",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2021, 2, 1),
                    BasicSalary = 4500,
                    CheckInTime = new TimeSpan(10, 0, 0),
                    CheckOutTime = new TimeSpan(18, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 7,
                    FullName = "Kareem ali",
                    Address = "Cairo",
                    Phone = "01022334455",
                    BirthDate = new DateTime(1985, 9, 25),
                    Gender = "Male",
                    NationalId = "28509251234563",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2010, 7, 20),
                    BasicSalary = 15000,
                    CheckInTime = new TimeSpan(8, 0, 0),
                    CheckOutTime = new TimeSpan(16, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                },
                new Employee
                {
                    Id = 8,
                    FullName = "Reem Adel",
                    Address = "Giza",
                    Phone = "01155667788",
                    BirthDate = new DateTime(1992, 4, 12),
                    Gender = "Female",
                    NationalId = "29204121234564",
                    Nationality = "Egyptian",
                    ContractDate = new DateTime(2019, 11, 3),
                    BasicSalary = 7000,
                    CheckInTime = new TimeSpan(9, 0, 0),
                    CheckOutTime = new TimeSpan(17, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2021, 11, 8),
                    UpdatedAt = new DateTime(2021, 11, 8)
                }
            );
        } 
        #endregion
    }
}

