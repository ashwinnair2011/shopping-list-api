using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shopping_list_api.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "PermissionDesc", "PermissionName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4718), null, null, "Administrator permission", "ADMIN" },
                    { 2, 1, new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4719), null, null, "Can log in to API", "canLogInToApi" }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "PermissionId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4707), "[\"ADMIN\",\"canLogInToApi\"]", "$2a$11$Ic.wgZJ3fn6F85nDWz145elgTj/HacVQQOGsmGQfHcjuXiNnlnmqa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 11, 30, 56, 353, DateTimeKind.Utc).AddTicks(4353), "[\"ADMIN\"]", "$2a$11$X/97WLnh.HPwpb3Imz1ZEuDJL7dPx3WqhAymGqutj.XSxaUWpzAum" });
        }
    }
}
