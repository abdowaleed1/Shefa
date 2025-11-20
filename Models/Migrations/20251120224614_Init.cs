using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    manager_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.id);
                    table.ForeignKey(
                        name: "FK_Clinic_Users_manager_id",
                        column: x => x.manager_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.id);
                    table.ForeignKey(
                        name: "FK_Patient_Users_id",
                        column: x => x.id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    consultation_price = table.Column<decimal>(type: "money", nullable: false),
                    consultation_time = table.Column<int>(type: "int", nullable: false),
                    average_review_rate = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Count_of_reviews = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    education = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    experience_years = table.Column<int>(type: "int", nullable: true),
                    clinic_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctor_Clinic_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "Clinic",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Doctor_Users_id",
                        column: x => x.id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSchedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    recurrence_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    frequency = table.Column<int>(type: "int", nullable: false),
                    medication_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    next_run_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_delivered = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSchedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_NotificationSchedule_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    doctor_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "date", nullable: false),
                    confirmation_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    consultation_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    payment_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointment_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisReport",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    report_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    report_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisReport", x => x.id);
                    table.ForeignKey(
                        name: "FK_DiagnosisReport_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DiagnosisReport_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    doctor_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    clinic_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    day_of_week = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_Clinic_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "Clinic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    prescription_image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prescription_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prescription_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    doctor_comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    clinic_comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    doctor_rating = table.Column<int>(type: "int", nullable: false),
                    clinic_rating = table.Column<int>(type: "int", nullable: false),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    doctor_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    clinic_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.id);
                    table.ForeignKey(
                        name: "FK_Review_Clinic_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "Clinic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Doctor_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientNotes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    appointment_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    note_content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    note_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientNotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_PatientNotes_Appointment_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "Appointment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PatientNotes_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    transaction_reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    patient_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    appointment_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    AppointmentId1 = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transaction_Appointment_AppointmentId1",
                        column: x => x.AppointmentId1,
                        principalTable: "Appointment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transaction_Appointment_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "Appointment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transaction_Patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patient_id",
                table: "Appointment",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_manager_id",
                table: "Clinic",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisReport_doctor_id",
                table: "DiagnosisReport",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisReport_patient_id",
                table: "DiagnosisReport",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_clinic_id",
                table: "Doctor",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_clinic_id",
                table: "DoctorSchedule",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_doctor_id_day_of_week_start_time_clinic_id",
                table: "DoctorSchedule",
                columns: new[] { "doctor_id", "day_of_week", "start_time", "clinic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSchedule_patient_id",
                table: "NotificationSchedule",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNotes_appointment_id",
                table: "PatientNotes",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNotes_patient_id",
                table: "PatientNotes",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_doctor_id",
                table: "Prescription",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_patient_id",
                table: "Prescription",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_clinic_id",
                table: "Review",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_doctor_id",
                table: "Review",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_patient_id_doctor_id",
                table: "Review",
                columns: new[] { "patient_id", "doctor_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_appointment_id",
                table: "Transaction",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AppointmentId1",
                table: "Transaction",
                column: "AppointmentId1",
                unique: true,
                filter: "[AppointmentId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_patient_id",
                table: "Transaction",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosisReport");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropTable(
                name: "NotificationSchedule");

            migrationBuilder.DropTable(
                name: "PatientNotes");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
