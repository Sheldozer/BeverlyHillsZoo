using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Elephant : IAnimal, IMakeSound // Inherit LandHabitat?
    {
        public string Name { get; set; }

        public Elephant(string name) 
        {
            Name = name;
        }

        public void MakeSound()
        {
            Console.WriteLine("The elephant trumpets");
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
