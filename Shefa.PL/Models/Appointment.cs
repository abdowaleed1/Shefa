using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        // --- Foreign Keys ---
        [Required]
        [Display(Name = "Patient")]
        public int PatientID { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        // --- Appointment Details ---
        [Required(ErrorMessage = "Appointment date and time is required.")]
        [Display(Name = "Appointment Date & Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentTime { get; set; }

        [StringLength(100)]
        public string Reason { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // e.g., "Scheduled", "Completed", "Cancelled"

        // --- Optional Info ---
        [EmailAddress]
        [Display(Name = "Patient Email")]
        public string PatientEmail { get; set; }

        [Display(Name = "Doctor Fee ($)")]
        [Range(0, int.MaxValue, ErrorMessage = "Fee must be a positive value.")]
        public int DoctorFee { get; set; }
        [Display(Name = "Additional Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Report Fee ($)")]
        [Range(0, int.MaxValue, ErrorMessage = "Fee must be a positive value.")]
        public int ReportFee { get; set; }

        // --- Navigation Properties ---
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }

        // --- Related Entities ---
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public virtual ICollection<MedicalReport> Reports { get; set; } = new List<MedicalReport>();

        // --- Computed (Not Mapped) Fields ---
        [NotMapped]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
