using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Tiger : Animal, IMakeSound // Inherit LandHabitat? Don't Inherit Animal?
    {
        public Tiger(string name)
        {
            Name = name;
        }

        public void MakeSound()
        {
            Console.WriteLine("The elephant trumpets");
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
