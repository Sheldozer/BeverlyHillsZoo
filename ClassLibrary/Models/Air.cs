using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Air : Habitat, IMakeSound
    {
        public int MaxAltitude { get; set; }

        public override void Move()
        {
            Console.WriteLine("I fly");
        }

        public void Sound()
        {
            Console.WriteLine("Air animal sound");
        }
    }
}
