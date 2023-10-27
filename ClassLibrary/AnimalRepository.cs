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
        private readonly ZooContext _context;
        public AnimalRepository(ZooContext context)
        {
            _context = context;
        }
        public void AddAnimal(Animal animal)
        {
                _context.Add(animal);
                _context.SaveChanges();
        }

        public void UpdateAnimal(Animal animal) 
        { 
            _context.Update(animal);
            _context.SaveChanges();
        }
        public void DeleteAnimal(Animal animal) 
        {
            _context.Remove(animal);
            _context.SaveChanges();
        }

        public List<Animal> GetAnimals()
        {
            return _context.Animals.ToList();
        }
        public Animal GetAnimalById(int id)
        {
            return _context.Animals.FirstOrDefault(a => a.Id == id);
        }

        public void SeedAnimals()
        {
            // if there are no animals
            if (!_context.Animals.Any())
            {
                // Water animals
                var electricEel = new Water
                {
                    Name = "Electric Eel",
                    DivingDepth = 50
                };
                _context.Add(electricEel);

                var dolphin = new Water
                {
                    Name = "Dolphin",
                    DivingDepth = 100
                };
                _context.Add(dolphin);
                // Air animals
                var norweiganBlueParrot = new Air
                {
                    Name = "Norweigan Blue Parrot",
                    MaxAltitude = 100
                };
                _context.Add(norweiganBlueParrot);
                var baldEagle = new Air
                {
                    Name = "Bald Eagle",
                    MaxAltitude = 300
                };
                _context.Add(baldEagle);
                // Land animals
                var elephant = new Land
                {
                    Name = "Elephant",
                    Speed = 40
                };
                _context.Add(elephant);
                var panther = new Land
                {
                    Name = "Panther",
                    Speed = 60
                };
                _context.Add(panther);

                _context.SaveChanges();
            }
        }

    }
}
