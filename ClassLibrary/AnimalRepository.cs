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

        public void UpdateAnimal() 
        { 
            var animals = _context.Animals.ToList();
            var animalToupdate = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("Choose Animal To Update")
                .PageSize(10)
                .UseConverter(animal => animal.Name)
                .AddChoices(animals));

            if (animalToupdate is Air airAnimal)
            {
                var newMaxAltitude = AnsiConsole.Ask<int>("Enter new max altitude for the animal: ");
                airAnimal.MaxAltitude = newMaxAltitude;
            }
            else if (animalToupdate is Water waterAnimal)
            {
                var newDivingDepth = AnsiConsole.Ask<int>("Enter new diving depth for the animal: ");
                waterAnimal.DivingDepth = newDivingDepth;
            }
            else if (animalToupdate is Land landAnimal)
            {
                var newSpeed = AnsiConsole.Ask<int>("Enter new speed for the animal: ");
                landAnimal.Speed = newSpeed;
            }

            _context.SaveChanges();
        }
        public void DeleteAnimal() 
        {
            var animals = _context.Animals.ToList();
            var animalToDelete = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .PageSize(10)
                .UseConverter(animal => animal.Name)
                .AddChoices(animals));

            _context.Remove(animalToDelete);
            _context.SaveChanges();
            AnsiConsole.MarkupLine("[green]Animal deleted sucsessfully[/]\n");
        }
        public void ViewAnimals()
        {
            var table = new Table();

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
