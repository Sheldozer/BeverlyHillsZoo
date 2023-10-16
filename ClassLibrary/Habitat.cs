using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class Habitat : Animal // Instruction says "Class “Habitat” Inherits Animal", but a habitat is not an animal?
    {
        public abstract override void Move();
    }
}
