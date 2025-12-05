using System.Web.Mvc;
using System.Web.Routing;

namespace SmartHealthcare
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // ========================================
            // ACCOUNT ROUTES
            // ========================================
            routes.MapRoute(
                name: "AccountIndex",
                url: "Account",
                defaults: new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                name: "AccountLogin",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "AccountRegister",
                url: "Account/Register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "AccountLogout",
                url: "Account/Logoff",
                defaults: new { controller = "Account", action = "Logoff" }
            );

            // ========================================
            // ADMIN ROUTES
            // ========================================
            routes.MapRoute(
                name: "AdminDashboard",
                url: "Admin/Dashboard",
                defaults: new { controller = "Admin", action = "Dashboard" }
            );

            routes.MapRoute(
                name: "AdminDoctors",
                url: "Admin/Doctors",
                defaults: new { controller = "Admin", action = "Doctors" }
            );

            routes.MapRoute(
                name: "AdminPatients",
                url: "Admin/Patients",
                defaults: new { controller = "Admin", action = "Patients" }
            );

            routes.MapRoute(
                name: "AdminAppointments",
                url: "Admin/Appointments",
                defaults: new { controller = "Admin", action = "Appointments" }
            );

            routes.MapRoute(
                name: "AdminBills",
                url: "Admin/Bills",
                defaults: new { controller = "Admin", action = "Bills" }
            );

            routes.MapRoute(
                name: "AdminMedicalReports",
                url: "Admin/MedicalReports",
                defaults: new { controller = "Admin", action = "MedicalReports" }
            );

            routes.MapRoute(
                name: "AdminAddMedicalReport",
                url: "Admin/AddMedicalReport",
                defaults: new { controller = "Admin", action = "AddMedicalReport" }
            );

            routes.MapRoute(
                name: "AdminDeleteReport",
                url: "Admin/DeleteReport/{id}",
                defaults: new { controller = "Admin", action = "DeleteReport", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminLogout",
                url: "Admin/Logoff",
                defaults: new { controller = "Admin", action = "Logoff" }
            );

            // ========================================
            // DOCTOR ROUTES (Role-based Controller)
            // ========================================
            routes.MapRoute(
                name: "DoctorDashboard",
                url: "Doctor/Dashboard",
                defaults: new { controller = "Doctor", action = "Dashboard" }
            );

            routes.MapRoute(
                name: "DoctorCompleteProfile",
                url: "Doctor/CompleteProfile",
                defaults: new { controller = "Doctor", action = "CompleteProfile" }
            );

            routes.MapRoute(
                name: "DoctorEditProfile",
                url: "Doctor/EditProfile",
                defaults: new { controller = "Doctor", action = "EditProfile" }
            );

            routes.MapRoute(
                name: "DoctorAppointments",
                url: "Doctor/Appointments",
                defaults: new { controller = "Doctor", action = "Appointments" }
            );

            routes.MapRoute(
                name: "DoctorPrescriptions",
                url: "Doctor/Prescriptions",
                defaults: new { controller = "Doctor", action = "Prescriptions" }
            );

            routes.MapRoute(
                name: "DoctorBills",
                url: "Doctor/Bills",
                defaults: new { controller = "Doctor", action = "Bills" }
            );
            routes.MapRoute(
                name: "DoctorMyPatients",
                url: "Doctor/MyPatients",
                defaults: new { controller = "Doctor", action = "MyPatients" }
            );

            routes.MapRoute(
                name: "DoctorReports",
                url: "Doctor/Reports",
                defaults: new { controller = "Doctor", action = "Reports" }
            );

            routes.MapRoute(
                name: "DoctorSchedule",
                url: "Doctor/MySchedule",
                defaults: new { controller = "Doctor", action = "MySchedule" }
            );

            routes.MapRoute(
                name: "DoctorProfile",
                url: "Doctor/MyProfile",
                defaults: new { controller = "Doctor", action = "MyProfile" }
            );

            // ========================================
            // DOCTORS ROUTES (Admin Management)
            // ========================================
            routes.MapRoute(
                name: "DoctorsList",
                url: "Doctors",
                defaults: new { controller = "Doctors", action = "Index" }
            );

            routes.MapRoute(
                name: "DoctorDetails",
                url: "Doctors/Details/{id}",
                defaults: new { controller = "Doctors", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DoctorCreate",
                url: "Doctors/Create",
                defaults: new { controller = "Doctors", action = "Create" }
            );
           
            routes.MapRoute(
                name: "DoctorEdit",
                url: "Doctors/Edit/{id}",
                defaults: new { controller = "Doctors", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DoctorDelete",
                url: "Doctors/Delete/{id}",
                defaults: new { controller = "Doctors", action = "Delete", id = UrlParameter.Optional }
            );

            // ========================================
            // PATIENT ROUTES (Role-based Controller)
            // ========================================
            routes.MapRoute(
    name: "PatientDashboard",
    url: "Patient/Dashboard",
    defaults: new { controller = "Patient", action = "Dashboard" }
);

            routes.MapRoute(
                name: "PatientMyAppointments",
                url: "Patient/MyAppointments",
                defaults: new { controller = "Patient", action = "MyAppointments" }
            );

            routes.MapRoute(
                name: "PatientMyBills",
                url: "Patient/MyBills",
                defaults: new { controller = "Patient", action = "MyBills" }
            );

            routes.MapRoute(
                name: "PatientMyReports",
                url: "Patient/MyReports",
                defaults: new { controller = "Patient", action = "MyReports" }
            );

            routes.MapRoute(
                name: "PatientMyPrescriptions",
                url: "Patient/MyPrescriptions",
                defaults: new { controller = "Patient", action = "MyPrescriptions" }
            );

            // ✅ Add these two for profile management
            routes.MapRoute(
                name: "PatientMyProfile",
                url: "Patient/MyProfile",
                defaults: new { controller = "Patient", action = "MyProfile" }
            );

            routes.MapRoute(
                name: "PatientCompleteProfile",
                url: "Patient/CompleteProfile",
                defaults: new { controller = "Patient", action = "CompleteProfile" }
            );

            // ========================================
            // PATIENTS ROUTES (Admin Management)
            // ========================================
            routes.MapRoute(
                name: "PatientsList",
                url: "Patients",
                defaults: new { controller = "Patients", action = "Index" }
            );

            routes.MapRoute(
                name: "PatientDetails",
                url: "Patients/Details/{id}",
                defaults: new { controller = "Patients", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PatientCreate",
                url: "Patients/Create",
                defaults: new { controller = "Patients", action = "Create" }
            );

            routes.MapRoute(
                name: "PatientEdit",
                url: "Patients/Edit/{id}",
                defaults: new { controller = "Patients", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PatientDelete",
                url: "Patients/Delete/{id}",
                defaults: new { controller = "Patients", action = "Delete", id = UrlParameter.Optional }
            );

            // ========================================
            // APPOINTMENTS ROUTES
            // ========================================
            routes.MapRoute(
                name: "AppointmentsList",
                url: "Appointments",
                defaults: new { controller = "Appointments", action = "Index" }
            );

            routes.MapRoute(
                name: "AppointmentDetails",
                url: "Appointments/Details/{id}",
                defaults: new { controller = "Appointments", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AppointmentCreate",
                url: "Appointments/Create",
                defaults: new { controller = "Appointments", action = "Create" }
            );

            routes.MapRoute(
                name: "AppointmentEdit",
                url: "Appointments/Edit/{id}",
                defaults: new { controller = "Appointments", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AppointmentDelete",
                url: "Appointments/Delete/{id}",
                defaults: new { controller = "Appointments", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AppointmentUpdateStatus",
                url: "Appointments/UpdateStatus/{id}",
                defaults: new { controller = "Appointments", action = "UpdateStatus", id = UrlParameter.Optional }
            );

            // ========================================
            // PRESCRIPTIONS ROUTES
            // ========================================
            routes.MapRoute(
                name: "PrescriptionsList",
                url: "Prescriptions",
                defaults: new { controller = "Prescriptions", action = "Index" }
            );

            routes.MapRoute(
                name: "PrescriptionDetails",
                url: "Prescriptions/Details/{id}",
                defaults: new { controller = "Prescriptions", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PrescriptionCreate",
                url: "Prescriptions/Create",
                defaults: new { controller = "Prescriptions", action = "Create" }
            );

            routes.MapRoute(
                name: "PrescriptionEdit",
                url: "Prescriptions/Edit/{id}",
                defaults: new { controller = "Prescriptions", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PrescriptionDelete",
                url: "Prescriptions/Delete/{id}",
                defaults: new { controller = "Prescriptions", action = "Delete", id = UrlParameter.Optional }
            );

            // ========================================
            // BILLS ROUTES
            // ========================================
            routes.MapRoute(
                name: "BillsList",
                url: "Bills",
                defaults: new { controller = "Bills", action = "Index" }
            );

            routes.MapRoute(
                name: "BillDetails",
                url: "Bills/Details/{id}",
                defaults: new { controller = "Bills", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BillCreate",
                url: "Bills/Create",
                defaults: new { controller = "Bills", action = "Create" }
            );

            routes.MapRoute(
                name: "BillEdit",
                url: "Bills/Edit/{id}",
                defaults: new { controller = "Bills", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BillDelete",
                url: "Bills/Delete/{id}",
                defaults: new { controller = "Bills", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BillUpdatePaymentStatus",
                url: "Bills/UpdatePaymentStatus/{id}",
                defaults: new { controller = "Bills", action = "UpdatePaymentStatus", id = UrlParameter.Optional }
            );

            // ========================================
            // MEDICAL REPORTS ROUTES
            // ========================================
            routes.MapRoute(
                name: "MedicalReportsList",
                url: "MedicalReports",
                defaults: new { controller = "MedicalReports", action = "Index" }
            );

            routes.MapRoute(
                name: "MedicalReportDetails",
                url: "MedicalReports/Details/{id}",
                defaults: new { controller = "MedicalReports", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MedicalReportCreate",
                url: "MedicalReports/Create",
                defaults: new { controller = "MedicalReports", action = "Create" }
            );

            routes.MapRoute(
                name: "MedicalReportEdit",
                url: "MedicalReports/Edit/{id}",
                defaults: new { controller = "MedicalReports", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MedicalReportDelete",
                url: "MedicalReports/Delete/{id}",
                defaults: new { controller = "MedicalReports", action = "Delete", id = UrlParameter.Optional }
            );

            // ========================================
            // SCHEDULES ROUTES
            // ========================================
            routes.MapRoute(
                name: "SchedulesList",
                url: "Schedules",
                defaults: new { controller = "Schedules", action = "Index" }
            );

            routes.MapRoute(
                name: "ScheduleDetails",
                url: "Schedules/Details/{id}",
                defaults: new { controller = "Schedules", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ScheduleCreate",
                url: "Schedules/Create",
                defaults: new { controller = "Schedules", action = "Create" }
            );

            routes.MapRoute(
                name: "ScheduleEdit",
                url: "Schedules/Edit/{id}",
                defaults: new { controller = "Schedules", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ScheduleDelete",
                url: "Schedules/Delete/{id}",
                defaults: new { controller = "Schedules", action = "Delete", id = UrlParameter.Optional }
            );

            // ========================================
            // HOME ROUTES
            // ========================================
            routes.MapRoute(
                name: "HomeAbout",
                url: "Home/About",
                defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "HomeContact",
                url: "Home/Contact",
                defaults: new { controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
                name: "HomeReviews",
                url: "Home/Reviews",
                defaults: new { controller = "Home", action = "Reviews" }
            );

            routes.MapRoute(
                name: "HomeHelp",
                url: "Home/Help",
                defaults: new { controller = "Home", action = "Help" }
            );

            // ========================================
            // DEFAULT ROUTE (Keep this at the end)
            // ========================================
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}