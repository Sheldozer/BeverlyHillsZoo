using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Guide
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int GuideNumber { get; set; }
        public Competence GuideCompetence { get; set; } // Added the GuideCompetence property

        public enum Competence
        {
            Air,
            Land,
            Water
        }
    }
}
