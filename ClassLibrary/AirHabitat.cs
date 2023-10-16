using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AirHabitat : Habitat
    {
        public int MaxAltitude { get; set; }
        public override void Move()
        {
            Console.WriteLine("I fly");
        }
    }
}
