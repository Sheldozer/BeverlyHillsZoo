using ClassLibrary.Data;
using ClassLibrary.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AnimalRepository
    {
        public void AddAnimal()
        {
            using (var context = new ZooContext())
            {
                context.Animals.Add(animal);
                context.SaveChanges();
            }
        }

        public void UpdateAnimal() 
        { 

        }
        public void DeleteAnimal() 
        {

        }
        public void ViewAnimals()
        {

        }

        private void ReturnToMenu() // Returns back to the Main menu
        {


        }

    }
}
