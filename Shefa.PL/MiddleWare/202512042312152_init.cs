namespace SmartHealthCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        AppointmentTime = c.DateTime(nullable: false),
                        Reason = c.String(maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 20),
                        PatientEmail = c.String(),
                        DoctorFee = c.Int(nullable: false),
                        Notes = c.String(),
                        ReportFee = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        AppointmentID = c.Int(nullable: false),
                        DoctorFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReportFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentStatus = c.String(maxLength: 20),
                        DateIssued = c.DateTime(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        BillDate = c.DateTime(nullable: false),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.Appointments", t => t.AppointmentID)
                .Index(t => t.AppointmentID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Specialization = c.String(nullable: false, maxLength: 100),
                        MedicalLicenseNumber = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 150),
                        ContactNumber = c.String(nullable: false, maxLength: 15),
                        Address = c.String(maxLength: 200),
                        Experience = c.String(maxLength: 500),
                        Qualification = c.String(maxLength: 200),
                        ConsultationFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DoctorFee = c.Int(nullable: false),
                        ReportFee = c.Int(nullable: false),
                        HiringDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        RegistrationDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        DoctorID = c.Int(nullable: false),
                        DayOfWeek = c.String(nullable: false, maxLength: 10),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        LocationDetails = c.String(maxLength: 50),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Gender = c.String(nullable: false, maxLength: 10),
                        DateOfBirth = c.DateTime(),
                        ContactNumber = c.String(nullable: false, maxLength: 15),
                        Email = c.String(maxLength: 150),
                        Address = c.String(maxLength: 250),
                        BloodGroup = c.String(maxLength: 100),
                        Allergies = c.String(maxLength: 500),
                        MedicalHistory = c.String(maxLength: 2000),
                        RegistrationDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        EmergencyContact = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MedicalReports",
                c => new
                    {
                        MedicalReportID = c.Int(nullable: false, identity: true),
                        AppointmentID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        DoctorName = c.String(nullable: false, maxLength: 100),
                        PatientID = c.Int(nullable: false),
                        Diagnosis = c.String(nullable: false, maxLength: 250),
                        DoctorNotes = c.String(),
                        TestResults = c.String(),
                        ReportType = c.String(nullable: false, maxLength: 100),
                        FollowUpDate = c.DateTime(),
                        LabResults = c.String(),
                        Remarks = c.String(),
                        Treatment = c.String(),
                        ReportDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MedicalReportID)
                .ForeignKey("dbo.Appointments", t => t.AppointmentID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.AppointmentID)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionID = c.Int(nullable: false, identity: true),
                        AppointmentID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        Details = c.String(nullable: false, maxLength: 1000),
                        DrugName = c.String(nullable: false, maxLength: 100),
                        Dosage = c.String(nullable: false, maxLength: 50),
                        Instructions = c.String(nullable: false, maxLength: 250),
                        DateIssued = c.DateTime(nullable: false),
                        Medication = c.String(nullable: false, maxLength: 100),
                        Frequency = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrescriptionID)
                .ForeignKey("dbo.Appointments", t => t.AppointmentID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.AppointmentID)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Prescriptions", "AppointmentID", "dbo.Appointments");
            DropForeignKey("dbo.MedicalReports", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.MedicalReports", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.MedicalReports", "AppointmentID", "dbo.Appointments");
            DropForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Schedules", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bills", "AppointmentID", "dbo.Appointments");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Prescriptions", new[] { "PatientID" });
            DropIndex("dbo.Prescriptions", new[] { "DoctorID" });
            DropIndex("dbo.Prescriptions", new[] { "AppointmentID" });
            DropIndex("dbo.MedicalReports", new[] { "PatientID" });
            DropIndex("dbo.MedicalReports", new[] { "DoctorID" });
            DropIndex("dbo.MedicalReports", new[] { "AppointmentID" });
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.Schedules", new[] { "DoctorID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropIndex("dbo.Bills", new[] { "AppointmentID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.MedicalReports");
            DropTable("dbo.Patients");
            DropTable("dbo.Schedules");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bills");
            DropTable("dbo.Appointments");
        }
    }
}
