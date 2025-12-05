using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class MedicalReport
    {
        [Key]
        public int MedicalReportID { get; set; }

        // --- Foreign Key (Links to Appointment) ---
        [Required]
        [Display(Name = "Appointment ID")]
        public int AppointmentID { get; set; }

        // --- Doctor Info ---
        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Doctor name is required.")]
        [StringLength(100)]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        // --- Patient Info ---
        [Required]
        [Display(Name = "Patient ID")]
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        // --- Report Details ---
        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(250)]
        public string Diagnosis { get; set; }

        [Display(Name = "Doctor's Notes")]
        [DataType(DataType.MultilineText)]
        public string DoctorNotes { get; set; }

        [Display(Name = "Test Results")]
        [DataType(DataType.MultilineText)]
        public string TestResults { get; set; }

        [Required(ErrorMessage = "Report type is required.")]
        [StringLength(100)]
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Follow-up Date")]
        public DateTime? FollowUpDate { get; set; }

        [Display(Name = "Lab Results")]
        [DataType(DataType.MultilineText)]
        public string LabResults { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Treatment / Prescription")]
        [DataType(DataType.MultilineText)]
        public string Treatment { get; set; }

        [Display(Name = "Report Date")]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        // --- Navigation Properties ---
        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("AppointmentID")]
        public virtual Appointment Appointment { get; set; }
    }
}
