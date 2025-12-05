using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public async Task<ActionResult> Index()
        {
            var appointments = await db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.AppointmentTime)
                .ToListAsync();
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var appointment = await db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentID == id);

            if (appointment == null) return HttpNotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<ActionResult> Create()
        {
            // Get current logged-in user's email
            var userEmail = User.Identity.Name;

            // Find the patient record for this user
            var patient = await db.Patients.FirstOrDefaultAsync(p => p.Email == userEmail);

            if (patient == null)
            {
                TempData["Error"] = "Patient profile not found. Please complete your profile first.";
                return RedirectToAction("Index", "Home");
            }

            // Pass PatientID to the view (hidden)
            ViewBag.CurrentPatientID = patient.PatientID;

            // Populate doctor dropdown
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName");

            return View();
        }

        // GET: Get Doctor Fees by DoctorID (AJAX endpoint)
        [HttpGet]
        public async Task<JsonResult> GetDoctorFees(int doctorId)
        {
            var doctor = await db.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                return Json(new
                {
                    success = true,
                    doctorFee = doctor.DoctorFee,
                    reportFee = doctor.ReportFee
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Appointment appointment)
        {
            // Remove validation for fields set programmatically
            ModelState.Remove("PatientEmail");
            ModelState.Remove("CreatedAt");

            if (ModelState.IsValid)
            {
                try
                {
                    // Link patient email
                    var patient = await db.Patients.FindAsync(appointment.PatientID);
                    if (patient != null)
                    {
                        appointment.PatientEmail = patient.Email;
                    }

                    // Get doctor fees
                    var doctor = await db.Doctors.FindAsync(appointment.DoctorID);
                    if (doctor != null)
                    {
                        appointment.DoctorFee = doctor.DoctorFee;
                        appointment.ReportFee = doctor.ReportFee;
                    }

                    appointment.Status = "Scheduled";
                    appointment.CreatedAt = DateTime.Now;

                    db.Appointments.Add(appointment);
                    await db.SaveChangesAsync();

                    TempData["Success"] = "Appointment booked successfully!";
                    return RedirectToAction("MyAppointments", "Patient");
                }
                catch
                {
                    ViewBag.ErrorMessage = "Error booking appointment. Try again.";
                    ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", appointment.DoctorID);
                    return View(appointment);
                }
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", appointment.DoctorID);
            return View(appointment);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var appointment = await db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentID == id);

            if (appointment == null) return HttpNotFound();

            return View(appointment);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment updatedAppointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingAppointment = db.Appointments.Find(updatedAppointment.AppointmentID);

                    if (existingAppointment != null)
                    {
                        existingAppointment.AppointmentTime = updatedAppointment.AppointmentTime;

                        // Optional: Change the status to indicate it was rescheduled
                        existingAppointment.Status = "Rescheduled";

                        db.Entry(existingAppointment).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["SuccessMessage"] = $"Appointment {updatedAppointment.AppointmentID} successfully rescheduled to {updatedAppointment.AppointmentTime:f}.";
                    }

                    // --- Mock Success (REMOVE THIS IN YOUR REAL IMPLEMENTATION) ---
                    TempData["SuccessMessage"] = $"Mock success: Appointment {updatedAppointment.AppointmentID} rescheduled to {updatedAppointment.AppointmentTime:f}.";
                    // ------------------------------------------------------------


                    // --- 2. Redirect ---
                    return RedirectToAction("MyAppointments","Patient");

                }
                catch (Exception ex)
                {
                    // Log the exception (e.g., using a logging framework)
                    ModelState.AddModelError("", "Unable to save changes. Please try again or contact support. Error: " + ex.Message);
                }
            }

            // If ModelState is not valid or an exception occurred, return the view with the model.
            // NOTE: If you are using EF, you MUST re-populate the [NotMapped] fields like DoctorName 
            // and PatientName before returning the view, otherwise they will be blank.
            // Example: updatedAppointment.DoctorName = db.Doctors.Find(updatedAppointment.DoctorID).Name;
            // Example: updatedAppointment.PatientName = db.Patients.Find(updatedAppointment.PatientID).Name;
            return View(updatedAppointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
