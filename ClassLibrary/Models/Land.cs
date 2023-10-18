using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Land : Habitat, IMakeSound
    {
        public int Speed { get; set; }

        public override void Move()
        {
            Console.WriteLine("I run");
        }

        public void Sound()
        {
            Console.WriteLine("Land animal sound");
        }
    }
}
