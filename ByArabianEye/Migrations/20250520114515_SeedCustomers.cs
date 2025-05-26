using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ByArabianEye.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ArrivalDate", "ClientCode", "ClientName", "Country", "DepartureDate", "DriverName", "PhoneNumber", "To" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), "C001", "Ahmed Ali", "Saudi Arabia", new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), "Kamal", "0551234567", "Azerbaijan" },
                    { 2, new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), "C002", "Sara Mohammed", "UAE", new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "Murad", "0509876543", "Russia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
