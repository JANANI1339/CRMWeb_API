using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class seedtenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Tenants",
                newName: "IsDeleted");

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "CreatedDate", "EmailId", "IsDeleted", "TenantName" },
                values: new object[] { 1, new DateTime(2024, 3, 27, 11, 41, 56, 840, DateTimeKind.Local).AddTicks(5231), "", false, "Tenant1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Tenants",
                newName: "isDeleted");
        }
    }
}
