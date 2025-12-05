using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // ===============================
        // Dashboard
        // ===============================
        public async Task<ActionResult> Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
            {
                TempData["Info"] = "Please complete your profile first.";
                return RedirectToAction("CompleteProfile");
            }

            // Statistics
            ViewBag.PatientName = patient.FirstName + " " + patient.LastName;
            ViewBag.TotalAppointments = await _context.Appointments.CountAsync(a => a.PatientID == patient.PatientID);
            ViewBag.UpcomingAppointments = await _context.Appointments.CountAsync(a => a.PatientID == patient.PatientID &&
                a.AppointmentTime >= DateTime.Now && (a.Status == "Scheduled"|| a.Status == "Rescheduled"));
            ViewBag.CompletedAppointments = await _context.Appointments.CountAsync(a => a.PatientID == patient.PatientID && a.Status == "Completed");
            ViewBag.PendingBills = await _context.Bills.CountAsync(b => b.Appointment.PatientID == patient.PatientID && b.PaymentStatus == "Pending");

            // Recent appointments
            var recentAppointments = await _context.Appointments
                .Where(a => a.PatientID == patient.PatientID)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.AppointmentTime)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentAppointments = recentAppointments;

            return View(patient);
        }

        // ===============================
        // My Profile
        // ===============================
        public async Task<ActionResult> MyProfile()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            return View(patient);
        }

        // ===============================
        // Complete Profile (GET)
        // ===============================
        [HttpGet]
        public async Task<ActionResult> CompleteProfile()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient != null)
                return RedirectToAction("Dashboard");

            return View(new Patient());
        }

        // ===============================
        // Complete / Edit Profile (POST)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CompleteProfile(Patient model)
        {
            var userId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all required fields correctly.";
                return View(model);
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
            {
                // New patient
                model.UserId = userId;
                model.RegistrationDate = DateTime.Now;
                _context.Patients.Add(model);
            }
            else
            {
                // Update existing patient
                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;
                patient.Gender = model.Gender;
                patient.DateOfBirth = model.DateOfBirth;
                patient.ContactNumber = model.ContactNumber;
                patient.Email = model.Email;
                patient.Address = model.Address;
                patient.BloodGroup = model.BloodGroup;
                patient.Allergies = model.Allergies;
                patient.MedicalHistory = model.MedicalHistory;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Profile saved successfully!";
            return RedirectToAction("MyProfile");
        }

        // ===============================
        // Edit Profile (GET)
        // ===============================
        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            return View(patient);
        }

        // ===============================
        // Edit Profile (POST)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(Patient model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var patient = await _context.Patients.FindAsync(model.PatientID);
            if (patient == null)
                return HttpNotFound();

            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.Gender = model.Gender;
            patient.DateOfBirth = model.DateOfBirth;
            patient.ContactNumber = model.ContactNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;
            patient.BloodGroup = model.BloodGroup;
            patient.Allergies = model.Allergies;
            patient.MedicalHistory = model.MedicalHistory;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("MyProfile");
        }

        // ===============================
        // Book Appointment
        // ===============================
        public ActionResult BookAppointment()
        {
            return RedirectToAction("Create", "Appointments");
        }

        // ===============================
        // My Appointments
        // ===============================
        public async Task<ActionResult> MyAppointments()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            var appointments = await _context.Appointments
                .Where(a => a.PatientID == patient.PatientID)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.AppointmentTime)
                .ToListAsync();

            return View(appointments);
        }

        // ===============================
        // My Bills
        // ===============================
        public async Task<ActionResult> MyBills()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            var bills = await _context.Bills
                .Where(b => b.Appointment.PatientID == patient.PatientID)
                .Include(b => b.Appointment)
                .Include(b => b.Appointment.Doctor)
                .OrderByDescending(b => b.DateIssued)
                .ToListAsync();

            return View(bills);
        }

        // ===============================
        // My Reports
        // ===============================
        public async Task<ActionResult> MyReports()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            var reports = await _context.MedicalReports
                .Where(r => r.Appointment.PatientID == patient.PatientID)
                .Include(r => r.Appointment)
                .Include(r => r.Appointment.Doctor)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();

            return View(reports);
        }

        // ===============================
        // My Prescriptions
        // ===============================
        public async Task<ActionResult> MyPrescriptions()
        {
            var userId = User.Identity.GetUserId();
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return RedirectToAction("CompleteProfile");

            var prescriptions = await _context.Prescriptions
                .Where(p => p.Appointment.PatientID == patient.PatientID)
                .Include(p => p.Appointment)
                .Include(p => p.Appointment.Doctor)
                .OrderByDescending(p => p.DateIssued)
                .ToListAsync();

            return View(prescriptions);
        }

        // ===============================
        // Dispose
        // ===============================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
