﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Elephant : Animal, IMakeSound // Inherit LandHabitat? Don't Inherit Animal?
    {

        public Elephant(string name) 
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
