using Microsoft.EntityFrameworkCore.Migrations;

namespace hospital.Migrations
{
    public partial class CreateOPdsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OPDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    RoomBedId = table.Column<int>(type: "int", nullable: false),
                    DateAdmit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDischarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OPDs_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OPDs_RoomBeds_RoomBedId",
                        column: x => x.RoomBedId,
                        principalTable: "RoomBeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OPDs_DoctorId",
                table: "OPDs",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_OPDs_RoomBedId",
                table: "OPDs",
                column: "RoomBedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OPDs");
        }
    }
}
