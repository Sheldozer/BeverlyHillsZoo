using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class LandHabitat : Habitat
    {
        public int Speed { get; set; }
        public override void Move()
        {
            Console.WriteLine("I run");
        }
    }
}
