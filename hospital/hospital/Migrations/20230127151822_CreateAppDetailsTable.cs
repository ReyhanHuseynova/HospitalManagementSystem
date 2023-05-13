using Microsoft.EntityFrameworkCore.Migrations;

namespace hospital.Migrations
{
    public partial class CreateAppDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDetail_Appointments_AppointmentId",
                table: "AppDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDetail_Doctors_DoctorId",
                table: "AppDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDetail_Patients_PatientId",
                table: "AppDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDetail",
                table: "AppDetail");

            migrationBuilder.RenameTable(
                name: "AppDetail",
                newName: "AppDetails");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetail_PatientId",
                table: "AppDetails",
                newName: "IX_AppDetails_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetail_DoctorId",
                table: "AppDetails",
                newName: "IX_AppDetails_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetail_AppointmentId",
                table: "AppDetails",
                newName: "IX_AppDetails_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDetails",
                table: "AppDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetails_Appointments_AppointmentId",
                table: "AppDetails",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetails_Doctors_DoctorId",
                table: "AppDetails",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetails_Patients_PatientId",
                table: "AppDetails",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDetails_Appointments_AppointmentId",
                table: "AppDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDetails_Doctors_DoctorId",
                table: "AppDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDetails_Patients_PatientId",
                table: "AppDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDetails",
                table: "AppDetails");

            migrationBuilder.RenameTable(
                name: "AppDetails",
                newName: "AppDetail");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetails_PatientId",
                table: "AppDetail",
                newName: "IX_AppDetail_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetails_DoctorId",
                table: "AppDetail",
                newName: "IX_AppDetail_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDetails_AppointmentId",
                table: "AppDetail",
                newName: "IX_AppDetail_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDetail",
                table: "AppDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetail_Appointments_AppointmentId",
                table: "AppDetail",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetail_Doctors_DoctorId",
                table: "AppDetail",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDetail_Patients_PatientId",
                table: "AppDetail",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
