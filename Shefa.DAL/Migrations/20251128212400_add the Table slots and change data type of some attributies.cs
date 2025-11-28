using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class addtheTableslotsandchangedatatypeofsomeattributies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_doctor_id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "doctor_id",
                table: "Appointment");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "start_time",
                table: "DoctorSchedule",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "end_time",
                table: "DoctorSchedule",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "slot_duration_minutes",
                table: "DoctorSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SlotId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClinicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    is_booked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_blocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.id);
                    table.ForeignKey(
                        name: "FK_Slots_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slots_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SlotId",
                table: "Appointment",
                column: "SlotId",
                unique: true,
                filter: "[SlotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ClinicId",
                table: "Slots",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_DoctorId_date_start_time",
                table: "Slots",
                columns: new[] { "DoctorId", "date", "start_time" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Slots_SlotId",
                table: "Appointment",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Slots_SlotId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_SlotId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "slot_duration_minutes",
                table: "DoctorSchedule");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Appointment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "DoctorSchedule",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_time",
                table: "DoctorSchedule",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<string>(
                name: "doctor_id",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment",
                column: "doctor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_doctor_id",
                table: "Appointment",
                column: "doctor_id",
                principalTable: "Doctor",
                principalColumn: "id");
        }
    }
}
