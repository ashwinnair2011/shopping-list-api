using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopping_list_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingsPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "PermissionDesc", "PermissionName" },
                values: new object[] { 6, 1, new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6992), null, null, "Can view settings", "canViewSettings" });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "PermissionId", "UserId" },
                values: new object[] { 3, 6, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6943), "[\"ADMIN\",\"canLogInToApi\",\"canViewSettings\"]", "$2a$11$vPKPKjwxk8dyHLejC4nCxuemkGjXEmjIhSlgLaS08ibfQnXq1iUPO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4718));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 11, 57, 37, 784, DateTimeKind.Utc).AddTicks(4707), "[\"ADMIN\",\"canLogInToApi\"]", "$2a$11$Ic.wgZJ3fn6F85nDWz145elgTj/HacVQQOGsmGQfHcjuXiNnlnmqa" });
        }
    }
}
