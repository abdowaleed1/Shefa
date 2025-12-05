using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // ==============================================
        // ✅ Dashboard
        // ==============================================
        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                TempData["Info"] = "Please complete your profile before accessing the dashboard.";
                return RedirectToAction("CompleteProfile", "Doctor"); // ✅ FIXED ROUTE
            }

            ViewBag.TotalPatients = _context.Appointments
                .Where(a => a.DoctorID == doctor.DoctorID)
                .Select(a => a.PatientID)
                .Distinct()
                .Count();

            ViewBag.TotalReports = _context.MedicalReports
                .Count(r => r.DoctorID == doctor.DoctorID);

            ViewBag.TotalAppointments = _context.Appointments
                .Count(a => a.DoctorID == doctor.DoctorID);

            ViewBag.DoctorName = doctor.FullName;

            return View();
        }

        // ==============================================
        // ✅ Complete Profile (GET + POST)
        // ==============================================
        [HttpGet]
        public ActionResult CompleteProfile()
        {
            var userId = User.Identity.GetUserId();
            var existing = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (existing != null)
                return RedirectToAction("Dashboard");

            return View(new Doctor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteProfile(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            doctor.UserId = User.Identity.GetUserId();
            doctor.HiringDate = DateTime.Now;

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            TempData["Success"] = "Profile completed successfully!";
            return RedirectToAction("Dashboard");
        }

        // ==============================================
        // ✅ Edit Profile
        // ==============================================
        [HttpGet]
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            var existing = _context.Doctors.Find(doctor.DoctorID);
            if (existing == null)
                return HttpNotFound();

            existing.Title = doctor.Title;
            existing.FirstName = doctor.FirstName;
            existing.LastName = doctor.LastName;
            existing.Specialization = doctor.Specialization;
            existing.MedicalLicenseNumber = doctor.MedicalLicenseNumber;
            existing.Email = doctor.Email;
            existing.ContactNumber = doctor.ContactNumber;
            existing.Qualification = doctor.Qualification;
            existing.Experience = doctor.Experience;
            existing.ConsultationFee = doctor.ConsultationFee;
            existing.ReportFee = doctor.ReportFee;
            existing.DoctorFee = doctor.DoctorFee;
            existing.Address = doctor.Address;


//            _context.Doctors.AddOrUpdate(existing);
            _context.SaveChanges();
            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Dashboard");
        }

        // ==============================================
        // ✅ My Appointments
        // ==============================================
        public ActionResult MyAppointments()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            var appointments = _context.Appointments
                .Where(a => a.DoctorID == doctor.DoctorID)
                .OrderByDescending(a => a.AppointmentTime)
                .ToList();

            foreach (var a in appointments)
            {
                a.PatientName = a.Patient?.PatientName ?? "N/A";
                a.DoctorName = doctor.FullName;
            }

            return View("~/Views/Doctor/Appointments.cshtml", appointments);
        }

        // ==============================================
        // ✅ My Patients
        // ==============================================
        public ActionResult MyPatients()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            var patients = _context.Appointments
                .Where(a => a.DoctorID == doctor.DoctorID)
                .Select(a => a.Patient)
                .Distinct()
                .ToList();

            return View(patients);
        }

        // ==============================================
        // ✅ My Reports
        // ==============================================
        public ActionResult MyReports()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            var reports = _context.MedicalReports
                .Where(r => r.DoctorID == doctor.DoctorID)
                .OrderByDescending(r => r.ReportDate)
                .ToList();

            return View("Report", reports);
        }

        // ==============================================
        // ✅ My Schedule
        // ==============================================
        public ActionResult MySchedule()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            var schedule = _context.Schedules
                .Where(s => s.DoctorID == doctor.DoctorID)
                .OrderBy(s => s.DayOfWeek)
                .ToList();

            return View("MySchedule", schedule);
        }

        // ==============================================
        // ✅ My Profile
        // ==============================================
        public ActionResult MyProfile()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            return View("MyProfile", doctor);
        }

        // ==============================================
        // ✅ Helper: Populate Appointment Dropdown
        // ==============================================
        private void PopulateAppointmentDropdown(int doctorId, object selectedAppointment = null)
        {
            var doctorAppointmentsQuery = _context.Appointments
                .Where(a => a.DoctorID == doctorId && a.Status == "Completed")
                .Select(a => new
                {
                    a.AppointmentID,
                    a.Patient,
                    a.AppointmentTime
                })
                .OrderByDescending(a => a.AppointmentTime)
                .ToList();

            var appointmentItems = doctorAppointmentsQuery
                .Select(a => new SelectListItem
                {
                    Value = a.AppointmentID.ToString(),
                    Text = $"{a.Patient.PatientName} - {a.AppointmentTime:dd-MM-yyyy hh:mm tt}"
                });

            ViewBag.AppointmentID = new SelectList(appointmentItems, "Value", "Text", selectedAppointment);
        }

        // ==============================================
        // ✅ Bills
        // ==============================================
        public ActionResult Bills()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            var bills = _context.Bills
                .Where(b => b.DoctorID == doctor.DoctorID)
                .ToList();

            return View(bills);
        }

        [HttpGet]
        public ActionResult CreateBill()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            PopulateAppointmentDropdown(doctor.DoctorID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBill(Bill bill)
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                TempData["Error"] = "Doctor profile not found.";
                return RedirectToAction("CompleteProfile");
            }

            if (ModelState.IsValid)
            {
                bill.DoctorID = doctor.DoctorID;
                bill.BillDate = DateTime.Now;
                bill.PaymentStatus = "Unpaid";

                decimal subtotal = bill.DoctorFee + bill.ReportFee;
                decimal taxAmount = subtotal * bill.TaxRate;
                bill.TotalAmount = subtotal + taxAmount;

                _context.Bills.Add(bill);
                _context.SaveChanges();

                TempData["Success"] = "Bill created successfully!";
                return RedirectToAction("Bills");
            }

            PopulateAppointmentDropdown(doctor.DoctorID, bill.AppointmentID);
            TempData["Error"] = "Please correct the errors in the form.";
            return View(bill);
        }

        // ==============================================
        // ✅ Prescriptions
        // ==============================================
        public ActionResult Prescriptions()
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null)
                return RedirectToAction("CompleteProfile");

            PopulateAppointmentDropdown(doctor.DoctorID);
            ViewBag.Appointments = ViewBag.AppointmentID;

            var prescriptions = _context.Prescriptions
                .Where(p => p.DoctorID == doctor.DoctorID)
                .ToList()
                .OrderByDescending(p => p.PrescriptionDate)
                .ToList();

            return View("Prescription", prescriptions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrescription(Prescription model)
        {
            var userId = User.Identity.GetUserId();
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                TempData["Error"] = "Doctor profile not found.";
                return RedirectToAction("CompleteProfile");
            }

            if (ModelState.IsValid)
            {
                model.DoctorID = doctor.DoctorID;
                model.PrescriptionDate = DateTime.Now;

                _context.Prescriptions.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Prescription created successfully!";
                return RedirectToAction("Prescriptions");
            }

            PopulateAppointmentDropdown(doctor.DoctorID, model.AppointmentID);
            ViewBag.Appointments = ViewBag.AppointmentID;

            var prescriptions = _context.Prescriptions
                .Where(p => p.DoctorID == doctor.DoctorID)
                .ToList()
                .OrderByDescending(p => p.PrescriptionDate)
                .ToList();

            TempData["Error"] = "Please correct the errors in the form.";
            return View("Prescription", prescriptions);
        }

        // ==============================================
        // ✅ Cleanup
        // ==============================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
