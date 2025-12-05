using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        // --- Personal Info ---
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10)]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(15)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        // --- Medical Info ---
        [StringLength(100)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [StringLength(500)]
        [Display(Name = "Allergies")]
        public string Allergies { get; set; }

        [StringLength(2000)]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; }

        // --- System Info ---
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        // --- Navigation Properties ---
        // ✅ IMPORTANT: These collections are referenced in OnModelCreating
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<MedicalReport> MedicalReports { get; set; } = new List<MedicalReport>();
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // --- Computed / Display Properties ---
        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [NotMapped]
        [Display(Name = "Age")]
        public int? Age
        {
            get
            {
                if (!DateOfBirth.HasValue)
                    return null;

                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Value.Year;
                if (DateOfBirth.Value.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string PatientName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [NotMapped]
        [Display(Name = "Phone")]
        public string Phone
        {
            get { return ContactNumber; }
        }
        [StringLength(15)]
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

    }
}