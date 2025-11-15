using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediAgenda.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class alterApplicationUserAddNameComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "NameComplete",
                table: "ApplicationUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameComplete",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "ApplicationUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "ApplicationUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
