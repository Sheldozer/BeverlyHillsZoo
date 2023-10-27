using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary
{
    public class MenuRepository
    {
        private readonly ZooContext _context;

        private readonly AnimalRepository _animalRepo;
        private readonly VisitRepository _visitRepo;
        private readonly VisitorRepository _visitorRepo;
        private readonly GuideRepository _guideRepo;

        private readonly AnimalInputValidator _validator = new AnimalInputValidator();

        public MenuRepository(ZooContext context, AnimalRepository animalRepo, VisitRepository visitRepo, VisitorRepository visitorRepo, GuideRepository guideRepo) 
        {
            _context = context;
            _animalRepo = animalRepo;
            _visitRepo = visitRepo;
            _visitorRepo = visitorRepo;
            _guideRepo = guideRepo;

        }
        public  void MainMenu()
        {
            var mainMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]What do you want to do?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                        .AddChoices(
                            "Manage animals",
                            "Manage visitors",
                            "Manage guides",
                            "Book a visit"
                        ));

            switch (mainMenu)
            {
                case "Manage animals":
                    ManageAnimalsMenu();
                    break;
                case "Manage visitors":
                    ManageVisitorsMenu();
                    break;
                case "Manage guides":
                    ManageGuidesMenu();
                    break;
                case "Book a visit":
                    ManageVisitMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;

            }
        }

        // Animals part of menu
        public void ManageAnimalsMenu()
        {
            var animalMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Animal menu[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                        .AddChoices(
                            "Add animal",
                            "Update animal",
                            "Delete animal",
                            "View animals",
                            "View specific animal",
                            "Go back to main menu"
                        ));

            switch (animalMenu)
            {
                // Go to some user input where the user gets to create the animal
                case "Add animal":
                    AddAnimalMenu();
                    break;
                // Go to a menu which contains all the animals, choose which one to update
                case "Update animal":
                    UpdateAnimalMenu();
                    ManageAnimalsMenu();
                    break;
                // Go to a menu which contains all the animals, choose which one to delete
                case "Delete animal":
                    DeleteAnimalMenu();
                    ManageAnimalsMenu();
                    break;
                case "View animals":
                    ViewAnimals();
                    ManageAnimalsMenu();
                    break;
                case "View specific animal":
                    ViewSpecificAnimal();
                    ManageAnimalsMenu();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void AddAnimalMenu()
        {
            var animalType = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Choose Animal Type")
               .PageSize(4)
               .AddChoices("Air", "Water", "Land", "Go back"));

            if (animalType == "Go back")
            {
                ManageAnimalsMenu();
            }
            var name = AnsiConsole.Prompt(
                 new TextPrompt<string>("Enter animal name: ")
                 .Validate(_validator.ValidateAnimalName));




            switch (animalType)
            {
                case "Air":


                    var maxAltitude = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal max altitude")
                        .Validate(_validator.ValidateAltitude));

                    var airAnimal = new Air
                    {
                        Name = name,
                        MaxAltitude = maxAltitude
                    };
                    _animalRepo.AddAnimal(airAnimal);
                    break;

                case "Water":
                    var divingDepth = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal diving depth")
                        .Validate(_validator.ValidateDivingDepth));

                    var waterAnimal = new Water
                    {
                        Name = name,
                        DivingDepth = divingDepth
                    };
                    _animalRepo.AddAnimal(waterAnimal);
                    break;
                case "Land":
                    var speed = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal speed")
                        .Validate(_validator.ValidateSpeed));

                    var landAnimal = new Land
                    {
                        Name = name,
                        Speed = speed
                    };
                    _animalRepo.AddAnimal(landAnimal);
                    break;
            }
            ManageAnimalsMenu();
        }


        public void DeleteAnimalMenu()
        {
            var animals = _animalRepo.GetAnimals();
            // add a null choice that we turn into "Go back to menu"
            animals.Add(null);
            var animalToDelete = AnsiConsole.Prompt(
                new SelectionPrompt<Animal?>()
                .PageSize(15)
                .UseConverter(animal => animal?.Name ?? "Go back to menu")
                .AddChoices(animals));

            if (animalToDelete == null)
            {
                ManageAnimalsMenu();
            }

            _animalRepo.DeleteAnimal(animalToDelete);
            AnsiConsole.MarkupLine("[green]Animal deleted sucsessfully[/]\n");
        }

        public void UpdateAnimalMenu()
        {
            var animals = _animalRepo.GetAnimals();
            animals.Add(null);
            var animalToUpdate = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("Choose Animal To Update")
                .PageSize(10)
                .UseConverter(animal => animal?.Name ?? "Go back to menu")
                .AddChoices(animals));

            if (animalToUpdate == null)
            {
                ManageAnimalsMenu();
            }

            if (animalToUpdate is Air airAnimal)
            {
                var newMaxAltitude = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter new max altitude for the animal")
                    .Validate(altitude =>
                    {
                        if (altitude <= 0 || altitude > 1000)
                            return ValidationResult.Error("Please enter a valid altitude for the animal. (1-1000)");
                        return ValidationResult.Success();
                    }));
                airAnimal.MaxAltitude = newMaxAltitude;
            }
            else if (animalToUpdate is Water waterAnimal)
            {
                var newDivingDepth = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter new diving depth for the animal")
                    .Validate(depth =>
                    {
                        if (depth <= 0 || depth > 1000)
                            return ValidationResult.Error("Please enter a valid depth for the animal. (1-1000)");
                        return ValidationResult.Success();
                    }));
                waterAnimal.DivingDepth = newDivingDepth;
            }
            else if (animalToUpdate is Land landAnimal)
            {
                var newSpeed = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter new speed for the animal")
                    .Validate(speed =>
                    {
                        if (speed < 0 || speed > 200)
                            return ValidationResult.Error("Please enter a valid speed for the animal. (1-200)");
                        return ValidationResult.Success();
                    }));
                landAnimal.Speed = newSpeed;
            }

            _animalRepo.UpdateAnimal(animalToUpdate);
        }

        public void ViewAnimals()
        {
            var animals = _animalRepo.GetAnimals();
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
            table.AddColumn("Animal sound");

            foreach (var animal in animals)
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

        public void ViewSpecificAnimal()
        {
            var animals = _animalRepo.GetAnimals();

            var selectedAnimal = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("Please select an animal from the list")
                .PageSize(10)
                .AddChoices(animals)
                .UseConverter(animal => $"{animal.Id}. {animal.Name}"));

            AnsiConsole.MarkupLine($"You have selected {selectedAnimal.Name}\n");
            if (selectedAnimal is Water waterAnimal)
            {
                AnsiConsole.MarkupLine("Water animals do not make any sound");
                waterAnimal.Move();
            }
            else if (selectedAnimal is Air airAnimal)
            {
                airAnimal.Sound();
                airAnimal.Move();
            }
            else if (selectedAnimal is Land landAnimal)
            {
                landAnimal.Sound();
                landAnimal.Move();
            }
            AnsiConsole.MarkupLine ("[grey]Press any key to continue...[/]");
            Console.ReadKey();
        }
        // Visitors part of menu
        public void ManageVisitorsMenu()
        {
            var visitorMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Visitor menu[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                        .AddChoices(
                            "Add visitor",
                            "Update visitor",
                            "Delete visitor",
                            "View visitors",
                            "Go back to main menu"
                        ));

            switch (visitorMenu)
            {
                case "Add visitor":
                _visitorRepo.AddVisitor();
                    ManageVisitorsMenu();
                    break;
                case "Update visitor":
                    _visitorRepo.UpdateVisitor();
                    ManageVisitorsMenu();
                    break;
                case "Delete visitor":
                    _visitorRepo.DeleteVisitor();
                    ManageVisitorsMenu();
                    break;
                case "View visitors":
                    _visitorRepo.ViewVisitors();
                    ManageVisitorsMenu();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        

        // Guides part of menu

        public void ManageGuidesMenu()
        {
            var guideMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Guide menu[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                        .AddChoices(
                            "Add guide",
                            "Update guide",
                            "Delete guide",
                            "View guides",
                            "Go back to main menu"
                        ));

            switch (guideMenu)
            {
                case "Add guide":
                    _guideRepo.AddGuide();
                    ManageGuidesMenu();
                    break;
                case "Update guide":
                    _guideRepo.UpdateGuide();
                    ManageGuidesMenu();
                    break;
                case "Delete guide":
                    _guideRepo.DeleteGuide();
                    ManageGuidesMenu();
                    break;
                case "View guides":
                    _guideRepo.ViewGuides();
                    ManageGuidesMenu();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        // Visit part of menu

        public void ManageVisitMenu()
        {
            var bookAVisitMenu = AnsiConsole.Prompt(
                      new SelectionPrompt<string>()
                          .Title("[green]Visitor menu[/]")
                          .PageSize(10)
                          .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                          .AddChoices(
                              "Book a visit for tomorrow",
                              "Delete visit",
                              "View visits",
                              "View archived visits",
                              "Go back to main menu"
                          ));

            switch (bookAVisitMenu)
            {
                case "Book a visit for tomorrow":
                    BookVisit();
                    MainMenu();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                case "Delete visit":
                    FindDeleteVisit();
                    MainMenu(); 
                    break;
                case "View visits":
                    _visitRepo.ViewVisits();
                    MainMenu();
                    break;
                case "View archived visits":
                    VisitRepository.ViewArchivedVisits(_context);
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void BookVisit()
        {
            // Select animal to visit
            var animals = _animalRepo.GetAnimals();
            animals.Add(null);
            var selectedAnimal = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("Choose an animal to visit")
                .PageSize(10)
                .UseConverter(animal => animal?.Name ?? "Go back to menu")
                .AddChoices(animals));

            if (selectedAnimal == null)
            {
                return;
            }

            // Add visitors to the visit
            var visitors = _context.Visitors.Where(v => !v.Removed).ToList(); //Filter out all removed visitors
            var selectedVisitors = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Visitor>()
                .PageSize(10)
                .UseConverter(visitor => visitor.Name)
                .AddChoices(visitors));

            var selectedVisitorsIds = selectedVisitors.Select(visitor => visitor.Id).ToList();


            // Visit booked for the next day
            var visitDate = DateTime.Now.AddDays(1).Date;

            var visitTimeSlot = AnsiConsole.Prompt(
                new SelectionPrompt<Visit.TimeSlot>()
                .Title("Choose a time slot for the visit")
                .PageSize(3)
                .AddChoices(Visit.TimeSlot.Morning, Visit.TimeSlot.Afternoon));
            
            _visitRepo.AddVisit(selectedAnimal.Id, selectedVisitorsIds, visitDate, visitTimeSlot);

            //AnsiConsole.MarkupLine("[green]Visit booked successfully![/]");
            AnsiConsole.Markup("Press any key to continue...");
            Console.ReadKey();

        }
        public int FindDeleteVisit()
        {
            var currentVisitsInfo = _context.Visits
         .Where(v => v.Archived == false)
         .SelectMany(v => v.Visitors, (visit, visitor) => new { visit.Animal.Name, visit.VisitDate, VisitorName = visitor.Name, visit.VisitTimeSlot, visit.Id, visitor.PassNumber })
         .GroupBy(v => v.Id)
         .Select(g => g.ToList())
         .ToList(); //Gör detta till egen metod. Återanvänts 2ggr

            var menuItems = new List<string>();

            foreach (var visitInfo in currentVisitsInfo)
            {
                string visitorNames = "";
                foreach (var visitor in visitInfo)
                {
                    if (visitorNames == "")
                    {
                        visitorNames = visitor.VisitorName;
                    }
                    else
                    {
                        visitorNames += $", {visitor.VisitorName}";
                    }
                }
                menuItems.Add($"{visitInfo.First().Name} on {visitInfo.First().VisitDate.ToString("d")} with {visitorNames}");
            }

            menuItems.Add("Go back to menu");

            /* _visitRepo.DeleteVisit(selectedVisitID);*/ //Get the ID to the delete method

            
            //var visitsRepository = new VisitorRepository();

            var deleteVisitMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Delete Visit menu[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(menuItems)
            );

            if ( deleteVisitMenu == "Go back to menu" ) 
            {
                ManageVisitMenu();
            }

            int selectedVisitID = -1; //variable to pick up the id on visit to be deleted

            foreach (var visitInfo in currentVisitsInfo)
            {
                var menuItem = $"{visitInfo.First().Name} on {visitInfo.First().VisitDate} with ";
                if (deleteVisitMenu.StartsWith(menuItem))
                {
                    selectedVisitID = visitInfo.First().Id;
                    break;
                }
            }

   
            _visitRepo.DeleteVisit(selectedVisitID); //Get the ID to the delete method

            return selectedVisitID;

        }

    }
}
