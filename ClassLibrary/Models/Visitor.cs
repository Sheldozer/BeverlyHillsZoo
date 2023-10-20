using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // List of visits this visitor has done
        public ICollection<Visit> Visits { get; set; }
    }
}
