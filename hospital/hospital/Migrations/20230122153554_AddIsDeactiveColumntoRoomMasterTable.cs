using Microsoft.EntityFrameworkCore.Migrations;

namespace hospital.Migrations
{
    public partial class AddIsDeactiveColumntoRoomMasterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "RoomMasters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "RoomMasters");
        }
    }
}
