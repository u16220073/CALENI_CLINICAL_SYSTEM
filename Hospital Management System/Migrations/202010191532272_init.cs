namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Announcements = c.String(nullable: false),
                        AnnouncementFor = c.String(nullable: false),
                        End = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                        AppointmentDate = c.DateTime(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Problem = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FullName = c.String(),
                        EmailAddress = c.String(),
                        PhoneNo = c.String(),
                        Contact = c.String(),
                        Age = c.Int(nullable: false),
                        LevelOfEducation = c.String(),
                        Language = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                        MaritalStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRole = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        CentreName = c.String(),
                        PsychologistName = c.String(),
                        PsychologistId = c.Int(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistId, cascadeDelete: true)
                .Index(t => t.PsychologistId);
            
            CreateTable(
                "dbo.Psychologists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FullName = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNo = c.String(),
                        ContactNo = c.String(nullable: false),
                        Specialization = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        BloodGroup = c.String(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Education = c.String(),
                        Status = c.String(nullable: false),
                        Centre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Centres", t => t.Centre_Id)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Centre_Id);
            
            CreateTable(
                "dbo.Centres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuditTrials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Who = c.String(nullable: false),
                        Transaction = c.String(nullable: false),
                        Where = c.String(nullable: false),
                        When = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Complain = c.String(nullable: false),
                        Reply = c.String(),
                        ComplainDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Consultations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        PsychologistId = c.Int(nullable: false),
                        ConsultationDate = c.DateTime(),
                        Diagnosis = c.String(),
                        TreatmentPlan = c.String(),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.PsychologistId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        PatientAddress = c.String(),
                        PatientName = c.String(),
                        PatientNumber = c.String(),
                        PatientEmail = c.String(),
                        PatientGender = c.String(),
                        PsychologistName = c.String(),
                        PsychologistSpecialist = c.String(),
                        PsychologistContact = c.String(),
                        CentreContact = c.String(),
                        CentrLocation = c.String(),
                        CenterName = c.String(),
                        PsychologistId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(),
                        DateOfBirth = c.DateTime(),
                        ServiceRecived = c.String(),
                        HoursOfService = c.Int(nullable: false),
                        ServiceAmount = c.Int(nullable: false),
                        PaidbyMedicalAid = c.Int(nullable: false),
                        PayByCash = c.Int(nullable: false),
                        TotalDue = c.String(),
                        InvoiceRefNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.PsychologistId);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        DoctorName = c.String(),
                        DoctorSpecialization = c.String(),
                        PatientId = c.Int(nullable: false),
                        UserName = c.String(),
                        PatientName = c.String(),
                        PatientGender = c.String(),
                        PatientAge = c.String(),
                        MedicalTest1 = c.String(),
                        MedicalTest2 = c.String(),
                        MedicalTest3 = c.String(),
                        MedicalTest4 = c.String(),
                        Medicine1 = c.String(),
                        Morning1 = c.Boolean(nullable: false),
                        Afternoon1 = c.Boolean(nullable: false),
                        Evening1 = c.Boolean(nullable: false),
                        Medicine2 = c.String(),
                        Morning2 = c.Boolean(nullable: false),
                        Afternoon2 = c.Boolean(nullable: false),
                        Evening2 = c.Boolean(nullable: false),
                        Medicine3 = c.String(),
                        Morning3 = c.Boolean(nullable: false),
                        Afternoon3 = c.Boolean(nullable: false),
                        Evening3 = c.Boolean(nullable: false),
                        Medicine4 = c.String(),
                        Morning4 = c.Boolean(nullable: false),
                        Afternoon4 = c.Boolean(nullable: false),
                        Evening4 = c.Boolean(nullable: false),
                        Medicine5 = c.String(),
                        Morning5 = c.Boolean(nullable: false),
                        Afternoon5 = c.Boolean(nullable: false),
                        Evening5 = c.Boolean(nullable: false),
                        Medicine6 = c.String(),
                        Morning6 = c.Boolean(nullable: false),
                        Afternoon6 = c.Boolean(nullable: false),
                        Evening6 = c.Boolean(nullable: false),
                        Medicine7 = c.String(),
                        Morning7 = c.Boolean(nullable: false),
                        Afternoon7 = c.Boolean(nullable: false),
                        Evening7 = c.Boolean(nullable: false),
                        CheckUpAfterDays = c.Int(nullable: false),
                        PrescriptionAddDate = c.DateTime(nullable: false),
                        DoctorTiming = c.String(),
                        Psychologist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Psychologists", t => t.Psychologist_Id)
                .Index(t => t.PatientId)
                .Index(t => t.Psychologist_Id);
            
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
            DropForeignKey("dbo.Prescriptions", "Psychologist_Id", "dbo.Psychologists");
            DropForeignKey("dbo.Prescriptions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Payments", "PsychologistId", "dbo.Psychologists");
            DropForeignKey("dbo.Payments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Consultations", "PsychologistId", "dbo.Psychologists");
            DropForeignKey("dbo.Consultations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "PsychologistId", "dbo.Psychologists");
            DropForeignKey("dbo.Psychologists", "Centre_Id", "dbo.Centres");
            DropForeignKey("dbo.Psychologists", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Prescriptions", new[] { "Psychologist_Id" });
            DropIndex("dbo.Prescriptions", new[] { "PatientId" });
            DropIndex("dbo.Payments", new[] { "PsychologistId" });
            DropIndex("dbo.Payments", new[] { "PatientId" });
            DropIndex("dbo.Consultations", new[] { "PsychologistId" });
            DropIndex("dbo.Consultations", new[] { "PatientId" });
            DropIndex("dbo.Psychologists", new[] { "Centre_Id" });
            DropIndex("dbo.Psychologists", new[] { "ApplicationUserId" });
            DropIndex("dbo.Schedules", new[] { "PsychologistId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Patients", new[] { "ApplicationUserId" });
            DropIndex("dbo.Appointments", new[] { "ScheduleId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Payments");
            DropTable("dbo.Consultations");
            DropTable("dbo.Complaints");
            DropTable("dbo.AuditTrials");
            DropTable("dbo.Centres");
            DropTable("dbo.Psychologists");
            DropTable("dbo.Schedules");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
            DropTable("dbo.Announcements");
        }
    }
}
