using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        // --- Foreign Key: Appointment ---
        [Required]
        [Display(Name = "Appointment ID")]
        public int AppointmentID { get; set; }

        // --- Foreign Key: Doctor ---
        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorID { get; set; }

        // --- Foreign Key: Patient ---
        [Required]
        [Display(Name = "Patient ID")]
        public int PatientID { get; set; }

        // --- Prescription Details ---
        [Required(ErrorMessage = "Prescription details are required.")]
        [StringLength(1000)]
        [Display(Name = "Prescription Details")]
        public string Details { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Medication Name")]
        public string DrugName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Dosage")]
        public string Dosage { get; set; } // e.g., "10mg", "500ml"

        [Required]
        [StringLength(250)]
        [Display(Name = "Instructions")]
        public string Instructions { get; set; } // e.g., "Take once daily after food"

        [Display(Name = "Date Issued")]
        [DataType(DataType.Date)]
        public DateTime DateIssued { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        [Display(Name = "Medication")]
        public string Medication { get; set; }

        [Required]
        [Display(Name = "Frequency")]
        public string Frequency { get; set; } // e.g., "Twice daily"

        [Required]
        [Display(Name = "Duration (days)")]
        public int Duration { get; set; }

        // --- Navigation Properties ---
        [ForeignKey("AppointmentID")]
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }
        public object PrescriptionDate { get;  set; }
    }
}
