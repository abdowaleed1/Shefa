using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        // --- Foreign Key ---
        [Required]
        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }

        // --- Schedule Details ---
        [Required(ErrorMessage = "Day of the week is required.")]
        [StringLength(10)]
        [Display(Name = "Day of Week")]
        public string DayOfWeek { get; set; }  // Stores: "Monday", "Tuesday", etc.

        [Required(ErrorMessage = "Start time is required.")]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [StringLength(50)]
        [Display(Name = "Location")]
        public string LocationDetails { get; set; }  // e.g., "Room 101"

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;  // ✅ ADDED THIS PROPERTY

        // --- Navigation Property ---
        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }
    }
}