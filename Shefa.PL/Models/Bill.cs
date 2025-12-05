using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Models
{
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        // --- Foreign Key to Appointment ---
        [Required]
        public int AppointmentID { get; set; }

        // --- Billing Details ---
        [Display(Name = "Doctor Fee")]
        [DataType(DataType.Currency)]
        public decimal DoctorFee { get; set; }

        [Display(Name = "Report Fee")]
        [DataType(DataType.Currency)]
        public decimal ReportFee { get; set; }  // ✅ Added correctly

        [Display(Name = "Tax Rate (%)")]
        public decimal TaxRate { get; set; } = 0.05m; // ✅ Default 5%

        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Payment Status")]
        [StringLength(20)]
        public string PaymentStatus { get; set; } // Pending / Paid

        [Display(Name = "Date Issued")]
        [DataType(DataType.Date)]
        public DateTime DateIssued { get; set; } = DateTime.Now;

        // --- Navigation Property ---
        [ForeignKey(nameof(AppointmentID))]
        public virtual Appointment Appointment { get; set; }
        public int DoctorID { get; set; }
        public DateTime BillDate { get; set; }
        [Display(Name = "Details/Notes")]
        public string Details { get; set; }
    }
}
