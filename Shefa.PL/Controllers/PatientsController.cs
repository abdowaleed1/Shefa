using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // -------------------- INDEX (List of Patients) --------------------
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Index()
        {
            var patients = await db.Patients.ToListAsync();
            return View(patients);
        }

        // -------------------- DETAILS --------------------
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patient = await db.Patients.FindAsync(id);
            if (patient == null)
                return HttpNotFound();

            return View(patient);
        }

        // -------------------- CREATE (GET) --------------------
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // -------------------- CREATE (POST) --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.RegistrationDate = DateTime.Now;
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
                TempData["Success"] = "Patient added successfully!";
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // -------------------- EDIT (GET) --------------------
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patient = await db.Patients.FindAsync(id);
            if (patient == null)
                return HttpNotFound();

            return View(patient);
        }

        // -------------------- EDIT (POST) --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = "Patient updated successfully!";
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // -------------------- DELETE (GET) --------------------
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patient = await db.Patients.FindAsync(id);
            if (patient == null)
                return HttpNotFound();

            return View(patient);
        }

        // -------------------- DELETE (POST) --------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var patient = await db.Patients.FindAsync(id);
            if (patient != null)
            {
                db.Patients.Remove(patient);
                await db.SaveChangesAsync();
                TempData["Success"] = "Patient deleted successfully!";
            }

            return RedirectToAction("Index");
        }

        // -------------------- DISPOSE --------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}