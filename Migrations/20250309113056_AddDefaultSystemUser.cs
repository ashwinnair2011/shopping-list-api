using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopping_list_api.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSystemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Permissions", "UserName", "UserPassword", "UserToken" },
                values: new object[] { 1, 1, new DateTime(2025, 3, 9, 11, 30, 56, 353, DateTimeKind.Utc).AddTicks(4353), null, null, "[\"ADMIN\"]", "system.user", "$2a$11$X/97WLnh.HPwpb3Imz1ZEuDJL7dPx3WqhAymGqutj.XSxaUWpzAum", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
