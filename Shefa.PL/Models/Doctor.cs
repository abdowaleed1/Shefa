using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Doctor
    {
        // --- Primary Key ---
        [Key]
        public int DoctorID { get; set; }

        // --- Personal Information ---
        [Required(ErrorMessage = "Title is required (e.g., Dr., Prof.).")]
        [StringLength(10)]
        public string Title { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{Title} {FirstName} {LastName}";

        // --- Professional Information ---
        [Required(ErrorMessage = "Specialization is required.")]
        [StringLength(100)]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "License Number is required.")]
        [StringLength(20)]
        [Display(Name = "License No.")]
        public string MedicalLicenseNumber { get; set; }

        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact No.")]
        public string ContactNumber { get; set; }

        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(500)]
        [Display(Name = "Experience")]
        public string Experience { get; set; }

        [StringLength(200)]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Range(0, 10000)]
        [Display(Name = "Consultation Fee (₹)")]
        public decimal ConsultationFee { get; set; }

        // --- Fee Information ---
        [Range(0, 100000)]
        [Display(Name = "Doctor Fee (₹)")]
        public int DoctorFee { get; set; }

        [Range(0, 100000)]
        [Display(Name = "Report Fee (₹)")]
        public int ReportFee { get; set; }

        [NotMapped]
        [Display(Name = "Phone")]
        public string Phone
        {
            get { return ContactNumber; }
        }

        // --- System Information ---
        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; } = DateTime.Now;

        // --- Identity Relationship (Link to ASP.NET User) ---
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        // --- Navigation Properties ---
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        //public static implicit operator Doctor(Doctor v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}