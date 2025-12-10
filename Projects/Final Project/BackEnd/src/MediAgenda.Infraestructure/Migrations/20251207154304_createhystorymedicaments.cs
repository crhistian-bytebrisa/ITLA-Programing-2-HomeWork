using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediAgenda.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class createhystorymedicaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentMedicaments");

            migrationBuilder.CreateTable(
                name: "HistoryMedicaments",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartMedication = table.Column<DateOnly>(type: "date", nullable: false),
                    EndMedication = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryMedicaments", x => new { x.PatientId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_HistoryMedicaments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryMedicaments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryMedicaments_MedicineId",
                table: "HistoryMedicaments",
                column: "MedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryMedicaments");

            migrationBuilder.CreateTable(
                name: "CurrentMedicaments",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    EndMedication = table.Column<DateOnly>(type: "date", nullable: true),
                    StartMedication = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentMedicaments", x => new { x.PatientId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_CurrentMedicaments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentMedicaments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentMedicaments_MedicineId",
                table: "CurrentMedicaments",
                column: "MedicineId");
        }
    }
}
