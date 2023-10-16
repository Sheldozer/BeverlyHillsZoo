using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ElectricEel : Animal, IMakeSound // Inherit WaterHabitat? Don't Inherit Animal?
    {
        public ElectricEel(string name)
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
