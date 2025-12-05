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
    public class MedicalReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MedicalReports
        public async Task<ActionResult> Index()
        {
            var reports = await db.MedicalReports
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .Include(m => m.Appointment.Patient)
                .OrderByDescending(m => m.ReportDate)
                .ToListAsync();

            return View(reports);
        }

        // GET: MedicalReports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = await db.MedicalReports
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .Include(m => m.Appointment.Patient)
                .FirstOrDefaultAsync(m => m.MedicalReportID == id);

            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: MedicalReports/Create
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

        // POST: MedicalReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Create(MedicalReport report)
        {
            if (ModelState.IsValid)
            {
                // Get doctor name from DoctorID
                var doctor = await db.Doctors.FindAsync(report.DoctorID);
                if (doctor != null)
                {
                    report.DoctorName = doctor.FullName;
                }

                report.ReportDate = DateTime.Now;
                db.MedicalReports.Add(report);
                await db.SaveChangesAsync();
                TempData["Success"] = "Medical report created successfully!";
                return RedirectToAction("Index");
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", report.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", report.AppointmentID);
            return View(report);
        }

        // GET: MedicalReports/Edit/5
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = await db.MedicalReports.FindAsync(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", report.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", report.AppointmentID);
            return View(report);
        }

        // POST: MedicalReports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult> Edit(MedicalReport report)
        {
            if (ModelState.IsValid)
            {
                // Update doctor name
                var doctor = await db.Doctors.FindAsync(report.DoctorID);
                if (doctor != null)
                {
                    report.DoctorName = doctor.FullName;
                }

                db.Entry(report).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = "Medical report updated successfully!";
                return RedirectToAction("Index");
            }

            var doctors = await db.Doctors.ToListAsync();
            var appointments = await db.Appointments.ToListAsync();

            ViewBag.DoctorID = new SelectList(doctors, "DoctorID", "FullName", report.DoctorID);
            ViewBag.AppointmentID = new SelectList(appointments, "AppointmentID", "Reason", report.AppointmentID);
            return View(report);
        }

        // GET: MedicalReports/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = await db.MedicalReports
                .Include(m => m.Doctor)
                .Include(m => m.Appointment)
                .FirstOrDefaultAsync(m => m.MedicalReportID == id);

            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: MedicalReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var report = await db.MedicalReports.FindAsync(id);
            db.MedicalReports.Remove(report);
            await db.SaveChangesAsync();
            TempData["Success"] = "Medical report deleted successfully!";
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