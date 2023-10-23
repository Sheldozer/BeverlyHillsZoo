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
        private readonly ZooContext _context = new ZooContext();

        private readonly AnimalRepository _animalRepo = new AnimalRepository();
        private readonly VisitRepository _visitRepo = new VisitRepository();
        public void MainMenu()
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
                    _animalRepo.UpdateAnimal();
                    ManageAnimalsMenu();
                    break;
                // Go to a menu which contains all the animals, choose which one to delete
                case "Delete animal":
                    _animalRepo.DeleteAnimal();
                    ManageAnimalsMenu();
                    break;
                case "View animals":
                    _animalRepo.ViewAnimals();
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

            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter animal name: ")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) || input.Length > 50)
                        return ValidationResult.Error("Please enter a valid name for the animal. (1-50 chars)");
                    return ValidationResult.Success();
                }));


            var animalType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose Animal Type")
                .PageSize(4)
                .AddChoices("Air", "Water", "Land", "Go back"));

            switch (animalType)
            {
                case "Air":


                    var maxAltitude = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal max altitude")
                        .Validate(altitude =>
                        {
                            if (altitude <= 0 || altitude > 1000)
                                return ValidationResult.Error("Please enter a valid altitude for the animal. (1-1000)");
                            return ValidationResult.Success();
                        }));

                    var airAnimal = new Air
                    {
                        Name = name,
                        MaxAltitude = maxAltitude
                    };
                    _animalRepo.AddAnimal(airAnimal);
                    break;

                case "Water":
                    var divingDepth = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal max altitude")
                        .Validate(depth =>
                        {
                             if (depth <= 0 || depth > 1000)
                                return ValidationResult.Error("Please enter a valid depth for the animal. (1-1000)");
                             return ValidationResult.Success();
                        }));

                    var waterAnimal = new Water
                    {
                        Name = name,
                        DivingDepth = divingDepth
                    };
                    _animalRepo.AddAnimal(waterAnimal);
                    break;
                case "Land":
                    var speed = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter animal max altitude")
                        .Validate(speed =>
                        {
                            if (speed < 0 || speed > 200)
                                return ValidationResult.Error("Please enter a valid speed for the animal. (1-200)");
                            return ValidationResult.Success();
                        }));

                    var landAnimal = new Land
                    {
                        Name = name,
                        Speed = speed
                    };
                    _animalRepo.AddAnimal(landAnimal);
                    break;
                case "Go back":
                    ManageAnimalsMenu();
                    break;
            }
            ManageAnimalsMenu();
        }
        // Visitors part of menu
        public void ManageVisitorsMenu()
        {
            var visitorRepo = new VisitorRepository();
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
                visitorRepo.AddVisitor();
                    break;
                case "Update visitor":
                    visitorRepo.UpdateVisitor();
                    break;
                case "Delete visitor":
                    visitorRepo.DeleteVisitor();
                    break;
                case "View visitors":
                    visitorRepo.ViewVisitors();
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
            var guideRepository = new GuideRepository();
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
                    guideRepository.AddGuide();
                    break;
                case "Update guide":
                    guideRepository.UpdateGuide();
                    break;
                case "Delete guide":
                    guideRepository.DeleteGuide();
                    break;
                case "View guides":
                    guideRepository.ViewGuides();
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
                              "Go back to main menu"
                          ));

            switch (bookAVisitMenu)
            {
                case "Book a visit for tomorrow":
                    BookVisit();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                case "Delete visit":
                    _visitRepo.DeleteVisit();
                    MainMenu(); 
                    break;
                case "View visits":
                    _visitRepo.ViewVisits();
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
            var animals = _context.Animals.ToList();
            var selectedAnimal = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("Choose an animal to visit")
                .PageSize(10)
                .UseConverter(animal => animal.Name)
                .AddChoices(animals));

            if (selectedAnimal == null)
            {
                AnsiConsole.MarkupLine("[red]No animal selected![/]");
                return;
            }

            // Add visitors to the visit
            var visitors = _context.Visitors.ToList();
            var selectedVisitors = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Visitor>()
                .PageSize(10)
                .UseConverter(visitor => visitor.Name)
                .AddChoices(visitors));

            // Visit booked for the next day
            var visitDate = DateTime.Now.AddDays(1).Date;

            var visitTimeSlot = AnsiConsole.Prompt(
                new SelectionPrompt<Visit.TimeSlot>()
                .Title("Choose a time slot for the visit")
                .PageSize(3)
                .AddChoices(Visit.TimeSlot.Morning, Visit.TimeSlot.Afternoon));

            // Create the visit
            var newVisit = new Visit
            {
                AnimalId = selectedAnimal.Id,
                Visitors = selectedVisitors.ToList(),
                VisitDate = visitDate,
                VisitTimeSlot = visitTimeSlot
            };
            
            _visitRepo.AddVisit(newVisit);

            AnsiConsole.MarkupLine("[green]Visit booked successfully![/]");
            AnsiConsole.Markup("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
