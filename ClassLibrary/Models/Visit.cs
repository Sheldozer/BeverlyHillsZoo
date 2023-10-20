using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }
        public int AnimalId { get; set; }
        public DateTime VisitDate { get; set; }

        // Is it a Morning or Afternoon visit?
        public TimeSlot VisitTimeSlot { get; set; }

        // The list of visitors for this visit
        public ICollection<Visitor> Visitors { get; set; } 
        public virtual Animal Animal { get; set; }


        public enum TimeSlot
        {
            Morning,
            Afternoon
        }
    }
}
