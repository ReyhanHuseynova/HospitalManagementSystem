using Microsoft.EntityFrameworkCore.Migrations;

namespace hospital.Migrations
{
    public partial class AddImageColumnToDepartmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Departments");
        }
    }
}
