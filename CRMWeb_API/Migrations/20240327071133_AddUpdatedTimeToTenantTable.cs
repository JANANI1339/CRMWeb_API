using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedTimeToTenantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Tenants",
                newName: "UpdatedTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Tenants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2024, 3, 27, 12, 41, 33, 129, DateTimeKind.Local).AddTicks(3902), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Tenants",
                newName: "CreatedDate");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 11, 41, 56, 840, DateTimeKind.Local).AddTicks(5231));
        }
    }
}
