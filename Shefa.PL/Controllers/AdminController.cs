using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // ------------------ DASHBOARD ------------------
        public async Task<ActionResult> Dashboard()
        {
            ViewBag.TotalDoctors = await _context.Doctors.CountAsync();
            ViewBag.TotalPatients = await _context.Patients.CountAsync();
            ViewBag.TotalAppointments = await _context.Appointments.CountAsync();
            ViewBag.TotalReports = await _context.MedicalReports.CountAsync();
            ViewBag.TotalBills = await _context.Bills.CountAsync();
            return View();
        }

        // ------------------ DOCTORS ------------------
        public async Task<ActionResult> Doctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Doctor added successfully!";
                return RedirectToAction("Doctors");
            }
            return View(doctor);
        }

        // ------------------ PATIENTS ------------------
        public async Task<ActionResult> Patients()
        {
            var patients = await _context.Patients.ToListAsync();
            return View(patients);
        }

        [HttpGet]
        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Patient added successfully!";
                return RedirectToAction("Patients");
            }
            return View(patient);
        }

        // ------------------ APPOINTMENTS ------------------
        public async Task<ActionResult> Appointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.AppointmentTime)
                .ToListAsync();

            return View(appointments);
        }

        // ------------------ BILLS ------------------
        public async Task<ActionResult> Bills()
        {
            var bills = await _context.Bills
                .Include(b => b.Appointment)
                .Include(b => b.Appointment.Doctor)
                .Include(b => b.Appointment.Patient)
                .OrderByDescending(b => b.DateIssued)
                .ToListAsync();

            return View(bills);
        }

        [HttpGet]
        public async Task<ActionResult> AddBill()
        {
            ViewBag.Appointments = new SelectList(await _context.Appointments.ToListAsync(), "AppointmentID", "AppointmentID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBill(Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.DateIssued = DateTime.Now;
                _context.Bills.Add(bill);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Bill generated successfully!";
                return RedirectToAction("Bills");
            }

            ViewBag.Appointments = new SelectList(await _context.Appointments.ToListAsync(), "AppointmentID", "AppointmentID", bill.AppointmentID);
            return View(bill);
        }

        // ------------------ MEDICAL REPORTS ------------------
        public async Task<ActionResult> Reports()
        {
            var reports = await _context.MedicalReports
                .Include(r => r.Appointment)
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();

            return View(reports);
        }

        [HttpGet]
        public async Task<ActionResult> AddMedicalReport()
        {
            var completedAppointments = await _context.Appointments
                .Where(a => a.Status == "Completed")
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            ViewBag.Appointments = new SelectList(completedAppointments, "AppointmentID", "AppointmentID");
            ViewBag.Doctors = new SelectList(await _context.Doctors.ToListAsync(), "DoctorID", "FullName");
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "PatientID", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMedicalReport(MedicalReport report)
        {
            if (ModelState.IsValid)
            {
                report.ReportDate = DateTime.Now;
                _context.MedicalReports.Add(report);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Medical report added successfully!";
                return RedirectToAction("Reports");
            }

            ViewBag.Appointments = new SelectList(await _context.Appointments.ToListAsync(), "AppointmentID", "AppointmentID", report.AppointmentID);
            ViewBag.Doctors = new SelectList(await _context.Doctors.ToListAsync(), "DoctorID", "FullName", report.DoctorID);
            ViewBag.Patients = new SelectList(await _context.Patients.ToListAsync(), "PatientID", "FullName", report.PatientID);
            return View(report);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteReport(int id)
        {
            var report = await _context.MedicalReports.FindAsync(id);
            if (report == null)
                return HttpNotFound();

            _context.MedicalReports.Remove(report);
            await _context.SaveChangesAsync();

            TempData["Success"] = "🗑️ Medical report deleted successfully!";
            return RedirectToAction("Reports");
        }

        // ------------------ LOGOUT ------------------
        public ActionResult LogOff()
        {
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();

            // Clear AntiForgery cookie
            if (Request.Cookies["__RequestVerificationToken"] != null)
            {
                var cookie = new System.Web.HttpCookie("__RequestVerificationToken", "")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login", "Account");
        }

        // ------------------ DISPOSE ------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
