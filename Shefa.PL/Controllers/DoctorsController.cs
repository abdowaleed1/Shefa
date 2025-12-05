using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    /// <summary>
    /// Handles all Doctor CRUD operations - Admin only access
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // ==============================================
        // ✅ INDEX - List all doctors
        // ==============================================
        /// <summary>
        /// GET: /Doctors
        /// Displays list of all doctors in the system
        /// </summary>
        public async Task<ActionResult> Index()
        {
            try
            {
                var doctors = await _context.Doctors
                    .OrderBy(d => d.FirstName)
                    .ThenBy(d => d.LastName)
                    .ToListAsync();

                ViewBag.TotalDoctors = doctors.Count;
                return View(doctors);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Index Error: {ex.Message}");
                TempData["Error"] = "❌ Error loading doctors list.";
                return View(new System.Collections.Generic.List<Doctor>());
            }
        }

        // ==============================================
        // ✅ DETAILS - View single doctor details
        // ==============================================
        /// <summary>
        /// GET: /Doctors/Details/5
        /// Displays detailed information about a specific doctor
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "⚠️ Invalid doctor ID.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var doctor = await _context.Doctors
                    .Include(d => d.Appointments)
                    .Include(d => d.Schedules)
                    .FirstOrDefaultAsync(d => d.DoctorID == id);

                if (doctor == null)
                {
                    TempData["Error"] = "⚠️ Doctor not found.";
                    return HttpNotFound();
                }

                // Get statistics
                ViewBag.TotalAppointments = doctor.Appointments?.Count ?? 0;
                ViewBag.UpcomingAppointments = doctor.Appointments?
                    .Count(a => a.AppointmentTime > DateTime.Now && a.Status == "Scheduled") ?? 0;
                ViewBag.CompletedAppointments = doctor.Appointments?
                    .Count(a => a.Status == "Completed") ?? 0;
                ViewBag.TotalSchedules = doctor.Schedules?.Count ?? 0;

                return View(doctor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Details Error: {ex.Message}");
                TempData["Error"] = "❌ Error loading doctor details.";
                return RedirectToAction("Index");
            }
        }

        // ==============================================
        // ✅ CREATE (GET) - Show create form
        // ==============================================
        /// <summary>
        /// GET: /Doctors/Create
        /// Displays the form to add a new doctor
        /// </summary>
        public ActionResult Create()
        {
            // Initialize with default values if needed
            var doctor = new Doctor
            {
                HiringDate = DateTime.Now,
                Experience = "0",
                ConsultationFee = 500 // Default consultation fee
            };

            return View(doctor);
        }

        // ==============================================
        // ✅ CREATE (POST) - Save new doctor to database
        // ==============================================
        /// <summary>
        /// POST: /Doctors/Create
        /// Processes the form and saves the new doctor to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Doctor doctor)
        {
            // Remove validation for auto-generated or optional fields
            ModelState.Remove("DoctorID");
            ModelState.Remove("UserId");
            ModelState.Remove("HiringDate");
            ModelState.Remove("FullName");
            ModelState.Remove("Appointments");
            ModelState.Remove("Schedules");
            ModelState.Remove("MedicalReports");
            ModelState.Remove("Bills");
            ModelState.Remove("Prescriptions");

            // Validate model state
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error}");
                }

                TempData["Error"] = "❌ Please fill all required fields correctly.";
                return View(doctor);
            }

            try
            {
                // Check for duplicate email
                var existingDoctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.Email.ToLower() == doctor.Email.ToLower());

                if (existingDoctor != null)
                {
                    ModelState.AddModelError("Email", "A doctor with this email already exists.");
                    TempData["Error"] = "❌ A doctor with this email already exists.";
                    return View(doctor);
                }

                // Set auto-generated fields
                doctor.HiringDate = DateTime.Now;

                // Set DoctorFee and ReportFee if they're properties in your model
                // If these don't exist in your Doctor model, remove these lines
                try
                {
                    var doctorFeeProperty = doctor.GetType().GetProperty("DoctorFee");
                    if (doctorFeeProperty != null && doctorFeeProperty.GetValue(doctor) == null)
                    {
                        doctorFeeProperty.SetValue(doctor, doctor.ConsultationFee);
                    }

                    var reportFeeProperty = doctor.GetType().GetProperty("ReportFee");
                    if (reportFeeProperty != null && reportFeeProperty.GetValue(doctor) == null)
                    {
                        reportFeeProperty.SetValue(doctor, 100m); // Default report fee
                    }
                }
                catch
                {
                    // Properties don't exist, continue without them
                }

                // Add title prefix if not present
                if (string.IsNullOrEmpty(doctor.Title))
                {
                    doctor.Title = "Dr.";
                }

                // Log the doctor being added
                System.Diagnostics.Debug.WriteLine($"Adding doctor: {doctor.FirstName} {doctor.LastName}");

                // ✅ ADD TO DATABASE CONTEXT
                _context.Doctors.Add(doctor);

                // ✅ SAVE CHANGES TO DATABASE
                await _context.SaveChangesAsync();

                // ✅ VERIFY IT WAS SAVED
                System.Diagnostics.Debug.WriteLine($"✅ Doctor saved with ID: {doctor.DoctorID}");

                TempData["Success"] = $"✅ Dr. {doctor.FirstName} {doctor.LastName} added successfully!";
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                // Handle Entity Framework validation errors
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

                var fullErrorMessage = string.Join("; ", errorMessages);
                System.Diagnostics.Debug.WriteLine($"Validation Error: {fullErrorMessage}");

                TempData["Error"] = $"❌ Validation error: {fullErrorMessage}";
                return View(doctor);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update errors (e.g., constraint violations)
                System.Diagnostics.Debug.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
                TempData["Error"] = "❌ Database error. Please check all fields and try again.";
                return View(doctor);
            }
            catch (Exception ex)
            {
                // Handle any other errors
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                TempData["Error"] = $"❌ Error adding doctor: {ex.Message}";
                return View(doctor);
            }
        }

        // ==============================================
        // ✅ EDIT (GET) - Show edit form
        // ==============================================
        /// <summary>
        /// GET: /Doctors/Edit/5
        /// Displays the form to edit an existing doctor
        /// </summary>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "⚠️ Invalid doctor ID.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var doctor = await _context.Doctors.FindAsync(id);

                if (doctor == null)
                {
                    TempData["Error"] = "⚠️ Doctor not found.";
                    return HttpNotFound();
                }

                return View(doctor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Edit GET Error: {ex.Message}");
                TempData["Error"] = "❌ Error loading doctor information.";
                return RedirectToAction("Index");
            }
        }

        // ==============================================
        // ✅ EDIT (POST) - Update doctor in database
        // ==============================================
        /// <summary>
        /// POST: /Doctors/Edit/5
        /// Processes the form and updates the doctor in database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Doctor doctor)
        {
            // Remove validation for computed/navigation properties
            ModelState.Remove("FullName");
            ModelState.Remove("Appointments");
            ModelState.Remove("Schedules");
            ModelState.Remove("MedicalReports");
            ModelState.Remove("Bills");
            ModelState.Remove("Prescriptions");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error}");
                }

                TempData["Error"] = "❌ Please correct the errors in the form.";
                return View(doctor);
            }

            try
            {
                // Check if another doctor has the same email (excluding current doctor)
                var existingDoctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.Email.ToLower() == doctor.Email.ToLower()
                                           && d.DoctorID != doctor.DoctorID);

                if (existingDoctor != null)
                {
                    ModelState.AddModelError("Email", "Another doctor with this email already exists.");
                    TempData["Error"] = "❌ Another doctor with this email already exists.";
                    return View(doctor);
                }

                System.Diagnostics.Debug.WriteLine($"Updating doctor ID: {doctor.DoctorID}");

                // ✅ MARK ENTITY AS MODIFIED
                _context.Entry(doctor).State = EntityState.Modified;

                // ✅ SAVE CHANGES TO DATABASE
                await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"✅ Doctor updated successfully");

                TempData["Success"] = $"✅ Dr. {doctor.FirstName} {doctor.LastName} updated successfully!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts
                if (!await DoctorExists(doctor.DoctorID))
                {
                    TempData["Error"] = "⚠️ Doctor not found. It may have been deleted.";
                    return HttpNotFound();
                }
                else
                {
                    TempData["Error"] = "❌ Update conflict. Please try again.";
                    return View(doctor);
                }
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
                TempData["Error"] = "❌ Database error. Please check all fields and try again.";
                return View(doctor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Edit Error: {ex.Message}");
                TempData["Error"] = $"❌ Error updating doctor: {ex.Message}";
                return View(doctor);
            }
        }

        // ==============================================
        // ✅ DELETE (GET) - Show delete confirmation
        // ==============================================
        /// <summary>
        /// GET: /Doctors/Delete/5
        /// Displays confirmation page before deleting a doctor
        /// </summary>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "⚠️ Invalid doctor ID.";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var doctor = await _context.Doctors
                    .Include(d => d.Appointments)
                    .Include(d => d.Schedules)
                    .FirstOrDefaultAsync(d => d.DoctorID == id);

                if (doctor == null)
                {
                    TempData["Error"] = "⚠️ Doctor not found.";
                    return HttpNotFound();
                }

                // Show warning if doctor has appointments
                if (doctor.Appointments != null && doctor.Appointments.Any())
                {
                    ViewBag.HasAppointments = true;
                    ViewBag.AppointmentCount = doctor.Appointments.Count;
                }

                return View(doctor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete GET Error: {ex.Message}");
                TempData["Error"] = "❌ Error loading doctor information.";
                return RedirectToAction("Index");
            }
        }

        // ==============================================
        // ✅ DELETE (POST) - Remove doctor from database
        // ==============================================
        /// <summary>
        /// POST: /Doctors/Delete/5
        /// Deletes the doctor from the database
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var doctor = await _context.Doctors
                    .Include(d => d.Appointments)
                    .Include(d => d.Schedules)
                    .FirstOrDefaultAsync(d => d.DoctorID == id);

                if (doctor == null)
                {
                    TempData["Error"] = "⚠️ Doctor not found.";
                    return RedirectToAction("Index");
                }

                // Check for related records
                var hasAppointments = doctor.Appointments != null && doctor.Appointments.Any();

                if (hasAppointments)
                {
                    TempData["Error"] = $"❌ Cannot delete Dr. {doctor.FirstName} {doctor.LastName}. " +
                                       "This doctor has existing appointments. " +
                                       "Please delete or reassign appointments first.";
                    return RedirectToAction("Index");
                }

                var doctorName = $"{doctor.FirstName} {doctor.LastName}";
                System.Diagnostics.Debug.WriteLine($"Deleting doctor: {doctorName}");

                // ✅ REMOVE FROM DATABASE
                _context.Doctors.Remove(doctor);

                // ✅ SAVE CHANGES
                await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"✅ Doctor deleted successfully");

                TempData["Success"] = $"🗑️ Dr. {doctorName} removed successfully!";
            }
            catch (DbUpdateException ex)
            {
                // Handle foreign key constraint violations
                System.Diagnostics.Debug.WriteLine($"Delete Error: {ex.InnerException?.Message ?? ex.Message}");
                TempData["Error"] = "❌ Cannot delete this doctor. They have existing records (appointments, reports, etc.). " +
                                   "Please delete or reassign related records first.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete Error: {ex.Message}");
                TempData["Error"] = $"❌ Error deleting doctor: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // ==============================================
        // ✅ HELPER METHODS
        // ==============================================

        /// <summary>
        /// Checks if a doctor exists in the database
        /// </summary>
        private async Task<bool> DoctorExists(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.DoctorID == id);
        }

        /// <summary>
        /// Get doctor statistics (can be called from other controllers)
        /// </summary>
        public async Task<JsonResult> GetDoctorStats(int id)
        {
            try
            {
                var doctor = await _context.Doctors
                    .Include(d => d.Appointments)
                    .FirstOrDefaultAsync(d => d.DoctorID == id);

                if (doctor == null)
                {
                    return Json(new { success = false, message = "Doctor not found" }, JsonRequestBehavior.AllowGet);
                }

                var stats = new
                {
                    success = true,
                    doctorName = doctor.FullName,
                    totalAppointments = doctor.Appointments?.Count ?? 0,
                    upcomingAppointments = doctor.Appointments?
                        .Count(a => a.AppointmentTime > DateTime.Now && a.Status == "Scheduled") ?? 0,
                    completedAppointments = doctor.Appointments?
                        .Count(a => a.Status == "Completed") ?? 0,
                    consultationFee = doctor.ConsultationFee,
                    specialization = doctor.Specialization
                };

                return Json(stats, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetDoctorStats Error: {ex.Message}");
                return Json(new { success = false, message = "Error loading statistics" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Search doctors by name, specialization, or qualification
        /// </summary>
        [AllowAnonymous] // Allow patients to search doctors
        public async Task<ActionResult> Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return RedirectToAction("Index");
                }

                var doctors = await _context.Doctors
                    .Where(d => d.FirstName.Contains(searchTerm) ||
                               d.LastName.Contains(searchTerm) ||
                               d.Specialization.Contains(searchTerm) ||
                               d.Qualification.Contains(searchTerm))
                    .OrderBy(d => d.FirstName)
                    .ToListAsync();

                ViewBag.SearchTerm = searchTerm;
                ViewBag.ResultCount = doctors.Count;

                return View("Index", doctors);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Search Error: {ex.Message}");
                TempData["Error"] = "❌ Error performing search.";
                return RedirectToAction("Index");
            }
        }

        // ==============================================
        // ✅ DISPOSE - Clean up resources
        // ==============================================
        /// <summary>
        /// Disposes the database context when controller is disposed
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}