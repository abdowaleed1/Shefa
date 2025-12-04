using Models.Entities;
using System.Collections.Generic;

namespace Shefa.PL.Models
{
    public class DoctorDashboardViewModel
    {
        public Doctor? Doctor { get; set; }
        public List<Slot> Slots { get; set; } = new List<Slot>();
    }
}
