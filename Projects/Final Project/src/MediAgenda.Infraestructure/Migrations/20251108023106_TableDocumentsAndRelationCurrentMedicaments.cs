using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediAgenda.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class TableDocumentsAndRelationCurrentMedicaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotesConsultations_ConsultationId",
                table: "NotesConsultations");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "DaysAvailable",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "CurrentMedicaments",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    StartMedication = table.Column<DateOnly>(type: "date", nullable: false),
                    EndMedication = table.Column<DateOnly>(type: "date", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "MedicalDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalDocuments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesConsultations_ConsultationId",
                table: "NotesConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentMedicaments_MedicineId",
                table: "CurrentMedicaments",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDocuments_PatientId",
                table: "MedicalDocuments",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentMedicaments");

            migrationBuilder.DropTable(
                name: "MedicalDocuments");

            migrationBuilder.DropIndex(
                name: "IX_NotesConsultations_ConsultationId",
                table: "NotesConsultations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DaysAvailable",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_NotesConsultations_ConsultationId",
                table: "NotesConsultations",
                column: "ConsultationId");
        }
    }
}
