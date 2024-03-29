using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class TenantEmailIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 28, 12, 42, 57, 397, DateTimeKind.Local).AddTicks(7095));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 3, 27, 12, 41, 33, 129, DateTimeKind.Local).AddTicks(3902));
        }
    }
}
