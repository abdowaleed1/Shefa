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
    public class PrescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prescriptions
        public async Task<ActionResult> Index()
        {
            var prescriptions = await db.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .Include(p => p.Appointment.Patient)
                .OrderByDescending(p => p.DateIssued)
                .ToListAsync();

            return View(prescriptions);
        }

        // GET: Prescriptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prescription = await db.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .Include(p => p.Appointment.Patient)
                .FirstOrDefaultAsync(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Create()
        {
            var doctors = await db.Doctors.ToListAsync();
            var completedAppointments = await db.Appointments
                .Where(a => a.Status == "Completed")
                .ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName");
            ViewBag.AppointmentID = new SelectList(completedAppointments, "AppointmentID", "Reason");
            return View();
        }

        // POST: Prescriptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                prescription.DateIssued = DateTime.Now;
                db.Prescriptions.Add(prescription);
                await db.SaveChangesAsync();
                TempData["Success"] = "Prescription created successfully!";
                return RedirectToAction("Index");
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", prescription.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", prescription.AppointmentID);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", prescription.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", prescription.AppointmentID);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Edit(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = "Prescription updated successfully!";
                return RedirectToAction("Index");
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", prescription.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", prescription.AppointmentID);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prescription = await db.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var prescription = await db.Prescriptions.FindAsync(id);
            db.Prescriptions.Remove(prescription);
            await db.SaveChangesAsync();
            TempData["Success"] = "Prescription deleted successfully!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}