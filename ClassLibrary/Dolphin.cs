﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class Dolphin : Animal, IMakeSound // Inherit WaterHabitat? Don't Inherit Animal?
    {
        public Dolphin(string name)
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
