using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class NorweiganBlueParrot : Animal, IMakeSound // Inherit AirHabitat? Don't Inherit Animal?
    {
        public NorweiganBlueParrot(string name)
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
