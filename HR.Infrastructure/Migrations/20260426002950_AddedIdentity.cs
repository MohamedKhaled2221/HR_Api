using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    Screen = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPermissions_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Administrators", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "HR Staff", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "Id", "Action", "Screen", "UserGroupId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 1, 1 },
                    { 4, 4, 1, 1 },
                    { 5, 1, 2, 1 },
                    { 6, 2, 2, 1 },
                    { 7, 3, 2, 1 },
                    { 8, 4, 2, 1 },
                    { 9, 1, 3, 1 },
                    { 10, 2, 3, 1 },
                    { 11, 3, 3, 1 },
                    { 12, 4, 3, 1 },
                    { 13, 1, 4, 1 },
                    { 14, 2, 4, 1 },
                    { 15, 3, 4, 1 },
                    { 16, 4, 4, 1 },
                    { 17, 1, 5, 1 },
                    { 18, 2, 5, 1 },
                    { 19, 3, 5, 1 },
                    { 20, 4, 5, 1 },
                    { 21, 1, 6, 1 },
                    { 22, 2, 6, 1 },
                    { 23, 3, 6, 1 },
                    { 24, 4, 6, 1 },
                    { 25, 1, 7, 1 },
                    { 26, 2, 7, 1 },
                    { 27, 3, 7, 1 },
                    { 28, 4, 7, 1 },
                    { 29, 1, 1, 2 },
                    { 30, 2, 1, 2 },
                    { 31, 3, 1, 2 },
                    { 32, 4, 1, 2 },
                    { 33, 1, 2, 2 },
                    { 34, 2, 2, 2 },
                    { 35, 3, 2, 2 },
                    { 36, 4, 2, 2 },
                    { 37, 1, 3, 2 },
                    { 38, 2, 3, 2 },
                    { 39, 3, 3, 2 },
                    { 40, 4, 3, 2 },
                    { 41, 1, 4, 2 },
                    { 42, 2, 4, 2 },
                    { 43, 3, 4, 2 },
                    { 44, 4, 4, 2 },
                    { 45, 1, 5, 2 },
                    { 46, 2, 5, 2 },
                    { 47, 3, 5, 2 },
                    { 48, 4, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "UpdatedAt", "UserGroupId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@pioneers-solutions.com", "System Admin", true, "$2a$11$c84awI5hkIxiejS8ehyYoO4wifxC3QALC4vtmm7PEbz1IuZ8LdSay", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "admin" },
                    { 2, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "hr@pioneers-solutions.com", "HR Manager", true, "$2a$11$lxmRaOydGZpP2NQj/xwyUeU6hRcmBh9491u8jqXFFIV0get5lx92u", new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "hrmanager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_UserGroupId_Screen_Action",
                table: "GroupPermissions",
                columns: new[] { "UserGroupId", "Screen", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_Name",
                table: "UserGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGroupId",
                table: "Users",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");
        }
    }
}
