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

            AnsiConsole.MarkupLine("[green]Animal added sucsessfully[/]\n");

            
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
        public void ViewAnimals()
        {
            string title = "[yellow1]Current list of animals[/]";
            VisitRepository.PrintTitleTable(title);

            var table = new Table();
            table.Centered();

            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Type");
            table.AddColumn("Max Altitude");
            table.AddColumn("Speed");
            table.AddColumn("Diving Depth");

            foreach (var animal in _context.Animals)
            {
                if (animal is Air airAnimal)
                {
                    table.AddRow(animal.Id.ToString(), animal.Name, "Air", airAnimal.MaxAltitude.ToString(), "-", "-");
                }
                else if (animal is Water waterAnimal)
                {
                    table.AddRow(animal.Id.ToString(), animal.Name, "Water", "-", "-", waterAnimal.DivingDepth.ToString());
                }
                else if (animal is Land landAnimal)
                {
                    table.AddRow(animal.Id.ToString(), animal.Name, "Land", "-", landAnimal.Speed.ToString(), "-");
                }
            }
            AnsiConsole.Render(table);

            AnsiConsole.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
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
