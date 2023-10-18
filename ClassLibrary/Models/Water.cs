using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Water : Habitat
    {
        public int DivingDepth {  get; set; }

        public override void Move()
        {
            Console.WriteLine("I swim");
        }
    }
}
