using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int PassNumber {get; set;}

        public bool Removed { get; set; }
    }
}
