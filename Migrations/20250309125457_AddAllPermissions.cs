using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shopping_list_api.Migrations
{
    /// <inheritdoc />
    public partial class AddAllPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9821));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6,
                columns: new[] { "CreatedOn", "PermissionDesc", "PermissionName" },
                values: new object[] { new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9830), "Can modify shopping lists", "canModifyShoppingLists" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "PermissionDesc", "PermissionName" },
                values: new object[,]
                {
                    { 3, 1, new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9823), null, null, "Can view users", "canViewUsers" },
                    { 4, 1, new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9826), null, null, "Can modify users", "canModifyUsers" },
                    { 5, 1, new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9828), null, null, "Can view shopping lists", "canViewShoppingLists" },
                    { 7, 1, new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9832), null, null, "Can view settings", "canViewSettings" },
                    { 8, 1, new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9834), null, null, "Can modify settings", "canModifySettings" }
                });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 3,
                column: "PermissionId",
                value: 3);

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserPermissionId", "PermissionId", "UserId" },
                values: new object[,]
                {
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9807), "[\"ADMIN\",\"canLogInToApi\",\"canViewUsers\",\"canModifyUsers\",\"canViewShoppingLists\",\"canModifyShoppingLists\",\"canViewSettings\",\"canModifySettings\"]", "$2a$11$.yKLsBulzZew0UoQ1tZrQuy/B9jf5AgTy/Em.TZYlzH4ovXdGgVNG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 8);

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

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6,
                columns: new[] { "CreatedOn", "PermissionDesc", "PermissionName" },
                values: new object[] { new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6992), "Can view settings", "canViewSettings" });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "UserPermissionId",
                keyValue: 3,
                column: "PermissionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Permissions", "UserPassword" },
                values: new object[] { new DateTime(2025, 3, 9, 12, 50, 38, 496, DateTimeKind.Utc).AddTicks(6943), "[\"ADMIN\",\"canLogInToApi\",\"canViewSettings\"]", "$2a$11$vPKPKjwxk8dyHLejC4nCxuemkGjXEmjIhSlgLaS08ibfQnXq1iUPO" });
        }
    }
}
