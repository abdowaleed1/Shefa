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
    public class BillsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bills
        public async Task<ActionResult> Index()
        {
            var bills = await db.Bills
                .Include(b => b.Appointment)
                .Include(b => b.Appointment.Patient)
                .Include(b => b.Appointment.Doctor)
                .OrderByDescending(b => b.DateIssued)
                .ToListAsync();
            return View(bills);
        }

        // GET: Bills/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var bill = await db.Bills
                .Include(b => b.Appointment)
                .Include(b => b.Appointment.Patient)
                .Include(b => b.Appointment.Doctor)
                .FirstOrDefaultAsync(b => b.BillID == id);

            if (bill == null) return HttpNotFound();

            return View(bill);
        }

        // GET: Bills/Create
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Create()
        {
            // Fetch completed appointments and select only IDs + names
            var appointments = await db.Appointments
                .Where(a => a.Status == "Completed")
                .Select(a => new
                {
                    a.AppointmentID,
                    PatientFirstName = a.Patient.FirstName,
                    PatientLastName = a.Patient.LastName,
                    DoctorFirstName = a.Doctor.FirstName,
                    DoctorLastName = a.Doctor.LastName,
                    a.AppointmentTime
                })
                .ToListAsync();

            // Combine names in memory
            var appointmentList = appointments.Select(a => new
            {
                a.AppointmentID,
                DisplayText = $"{a.PatientFirstName} {a.PatientLastName} - {a.DoctorFirstName} {a.DoctorLastName} ({a.AppointmentTime:dd/MM/yyyy HH:mm})"
            }).ToList();

            ViewBag.AppointmentID = new SelectList(appointmentList, "AppointmentID", "DisplayText");

            return View();
        }

        // POST: Bills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                // Auto-calculate total
                var subtotal = bill.DoctorFee + bill.ReportFee;
                var tax = subtotal * bill.TaxRate;
                bill.TotalAmount = subtotal + tax;
                bill.DateIssued = DateTime.Now;
                bill.PaymentStatus = "Pending";

                db.Bills.Add(bill);
                await db.SaveChangesAsync();

                TempData["Success"] = "Bill created successfully!";
                return RedirectToAction("Index");
            }

            // Reload appointment list if validation fails
            var appointments = await db.Appointments
                .Where(a => a.Status == "Completed")
                .Select(a => new
                {
                    a.AppointmentID,
                    PatientFirstName = a.Patient.FirstName,
                    PatientLastName = a.Patient.LastName,
                    DoctorFirstName = a.Doctor.FirstName,
                    DoctorLastName = a.Doctor.LastName,
                    a.AppointmentTime
                })
                .ToListAsync();

            var appointmentList = appointments.Select(a => new
            {
                a.AppointmentID,
                DisplayText = $"{a.PatientFirstName} {a.PatientLastName} - {a.DoctorFirstName} {a.DoctorLastName} ({a.AppointmentTime:dd/MM/yyyy HH:mm})"
            }).ToList();

            ViewBag.AppointmentID = new SelectList(appointmentList, "AppointmentID", "DisplayText", bill.AppointmentID);

            return View(bill);
        }

        // GET: Bills/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var bill = await db.Bills.FindAsync(id);
            if (bill == null) return HttpNotFound();

            return View(bill);
        }

        // POST: Bills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Bill bill)
        {
            if (ModelState.IsValid)
            {
                var subtotal = bill.DoctorFee + bill.ReportFee;
                var tax = subtotal * bill.TaxRate;
                bill.TotalAmount = subtotal + tax;

                db.Entry(bill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = "Bill updated successfully!";
                return RedirectToAction("Index");
            }
            return View(bill);
        }

        // POST: Bills/UpdatePaymentStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdatePaymentStatus(int id, string status)
        {
            var bill = await db.Bills.FindAsync(id);
            if (bill != null)
            {
                bill.PaymentStatus = status;
                await db.SaveChangesAsync();
                TempData["Success"] = $"Payment status updated to {status}!";
            }
            return RedirectToAction("Index");
        }

        // GET: Bills/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var bill = await db.Bills
                .Include(b => b.Appointment)
                .Include(b => b.Appointment.Patient)
                .FirstOrDefaultAsync(b => b.BillID == id);

            if (bill == null) return HttpNotFound();

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var bill = await db.Bills.FindAsync(id);
            db.Bills.Remove(bill);
            await db.SaveChangesAsync();
            TempData["Success"] = "Bill deleted successfully!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
