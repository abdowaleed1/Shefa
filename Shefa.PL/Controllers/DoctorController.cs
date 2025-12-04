using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shefa.BLL.Interfaces;
using Models.Entities;
using Shefa.PL.Models;
using Models.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Shefa.PL.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        // GET: DoctorController
        public ActionResult Index(string doctorId)
        {
            // if no doctorId provided, try to infer from current user (if any)
            // fallback: use passed doctorId or show empty dashboard
            var model = new DoctorDashboardViewModel();

            if (string.IsNullOrEmpty(doctorId))
            {
                // try to find doctor by current user id claim
                var userId = User?.Identity?.Name;
                if (!string.IsNullOrEmpty(userId))
                {
                    var doctor = _unitOfWork.Doctors.Find(d => d.UserId == userId);
                    if (doctor != null)
                        doctorId = doctor.Id;
                }
            }

            if (!string.IsNullOrEmpty(doctorId))
            {
                var doctor = _unitOfWork.Doctors.Find(d => d.Id == doctorId);
                model.Doctor = doctor;

                var today = DateOnly.FromDateTime(DateTime.Now);

                var slots = _unitOfWork.Slots.FindAll(s => s.DoctorId == doctorId && s.Date == today, new[] { "Appointment", "Appointment.Patient" })
                    .OrderBy(s => s.StartTime)
                    .ToList();

                model.Slots = slots;
            }

            return View(model);
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(string id)
        {
            var doctor = _unitOfWork.Doctors.Find(d => d.Id == id);
            return View(doctor);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Doctor updated)
        {
            try
            {
                var doctor = _unitOfWork.Doctors.Find(d => d.Id == id);
                if (doctor == null) return NotFound();

                doctor.Biography = updated.Biography;
                doctor.ConsultationPrice = updated.ConsultationPrice;
                doctor.ConsultationTime = updated.ConsultationTime;
                doctor.Specialty = updated.Specialty;

                _unitOfWork.Doctors.Update(doctor);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index), new { doctorId = id });
            }
            catch
            {
                return View();
            }
        }

        // POST: DoctorController/SaveConsult
        [HttpPost]
        public IActionResult SaveConsult(string appointmentId, string noteContent)
        {
            try
            {
                if (string.IsNullOrEmpty(appointmentId))
                    return Json(new { success = false, message = "Missing appointmentId" });

                var appointment = _unitOfWork.Appointments.Find(a => a.Id == appointmentId);
                if (appointment == null)
                    return Json(new { success = false, message = "Appointment not found" });

                var note = new PatientNotes
                {
                    AppointmentId = appointment.Id,
                    PatientId = appointment.PatientId,
                    NoteContent = noteContent ?? string.Empty,
                    NoteType = PatientNoteType.ConsultationSummary
                };

                _unitOfWork.PatientNotes.Add(note);
                _unitOfWork.Save();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
