using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByArabianEye.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerNoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_CustomerId",
                table: "CustomerNotes",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerNotes");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
