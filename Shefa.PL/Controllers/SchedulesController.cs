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
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedules
        public async Task<ActionResult> Index()
        {
            var schedules = await db.Schedules
                .Include(s => s.Doctor)
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();

            return View(schedules);
        }

        // GET: Schedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var schedule = await db.Schedules
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.ScheduleID == id);

            if (schedule == null)
                return HttpNotFound();

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Create()
        {
            ViewBag.DoctorID = new SelectList(await db.Doctors.ToListAsync(), "DoctorID", "FullName");
            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                await db.SaveChangesAsync();
                TempData["Success"] = "Schedule created successfully!";
                return RedirectToAction("MySchedule", "Doctor");
            }

            ViewBag.DoctorID = new SelectList(await db.Doctors.ToListAsync(), "DoctorID", "FullName", schedule.DoctorID);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
                return HttpNotFound();

            ViewBag.DoctorID = new SelectList(await db.Doctors.ToListAsync(), "DoctorID", "FullName", schedule.DoctorID);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Success"] = "Schedule updated successfully!";
                return RedirectToAction("MySchedule", "Doctor");
            }

            ViewBag.DoctorID = new SelectList(await db.Doctors.ToListAsync(), "DoctorID", "FullName", schedule.DoctorID);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var schedule = await db.Schedules
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.ScheduleID == id);

            if (schedule == null)
                return HttpNotFound();

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var schedule = await db.Schedules.FindAsync(id);
            db.Schedules.Remove(schedule);
            await db.SaveChangesAsync();
            TempData["Success"] = "Schedule deleted successfully!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
