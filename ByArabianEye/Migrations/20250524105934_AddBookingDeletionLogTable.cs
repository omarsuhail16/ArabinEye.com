using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByArabianEye.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDeletionLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDeletionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDeletionLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDeletionLogs");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
