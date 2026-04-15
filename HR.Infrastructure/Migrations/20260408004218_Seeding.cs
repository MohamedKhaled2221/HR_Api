using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BasicSalary", "BirthDate", "CheckInTime", "CheckOutTime", "ContractDate", "CreatedAt", "FullName", "Gender", "IsActive", "NationalId", "Nationality", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Cairo", 8000m, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2015, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohamed Ahmed Ismail", "Male", true, "29005151234567", "Egyptian", "01012345678", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "اGiza, Doki", 6500m, new DateTime(1993, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2018, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara Mahmoud", "Female", true, "29308221234568", "Egyptian", "01123456789", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Alexandria", 12000m, new DateTime(1988, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 30, 0, 0), new TimeSpan(0, 16, 30, 0, 0), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed Khaled Hassan", "Male", true, "28801101234569", "Egyptian", "01234567890", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Cairo", 5000m, new DateTime(1995, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2020, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Menna Ibrahim", "Female", true, "29503301234560", "Egyptian", "01098765432", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "October", 9500m, new DateTime(1991, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2016, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar Yousef", "Male", true, "29111051234561", "Egyptian", "01511223344", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Maadi", 4500m, new DateTime(1997, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nourahan Ali", "Female", true, "29706181234562", "Egyptian", "01677889900", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Cairo", 15000m, new DateTime(1985, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 16, 0, 0, 0), new DateTime(2010, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kareem ali", "Male", true, "28509251234563", "Egyptian", "01022334455", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Giza", 7000m, new DateTime(1992, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2019, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reem Adel", "Female", true, "29204121234564", "Egyptian", "01155667788", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
