using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class initDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    manager_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.id);
                    table.ForeignKey(
                        name: "FK_Clinic_AspNetUsers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.id);
                    table.ForeignKey(
                        name: "FK_Patient_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    consultation_price = table.Column<decimal>(type: "money", nullable: false),
                    consultation_time = table.Column<int>(type: "int", nullable: false),
                    average_review_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Count_of_reviews = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    education = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    experience_years = table.Column<int>(type: "int", nullable: true),
                    clinic_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Clinic_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "Clinic",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationSchedule",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    recurrence_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    frequency = table.Column<int>(type: "int", nullable: false),
                    medication_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    next_run_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_delivered = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    doctor_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    report_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    report_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    doctor_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    clinic_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    day_of_week = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    prescription_image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    doctor_comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    clinic_comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    doctor_rating = table.Column<int>(type: "int", nullable: false),
                    clinic_rating = table.Column<int>(type: "int", nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    doctor_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    clinic_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    appointment_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    transaction_reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    patient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    appointment_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Doctor_user_id",
                table: "Doctor",
                column: "user_id",
                unique: true);

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
                name: "IX_Patient_user_id",
                table: "Patient",
                column: "user_id",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
