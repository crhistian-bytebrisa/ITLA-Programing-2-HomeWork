using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediAgenda.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatemedicineprescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDosage",
                table: "PrescriptionsMedicines",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDosage",
                table: "PrescriptionsMedicines",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDosage",
                table: "PrescriptionsMedicines");

            migrationBuilder.DropColumn(
                name: "StartDosage",
                table: "PrescriptionsMedicines");
        }
    }
}
