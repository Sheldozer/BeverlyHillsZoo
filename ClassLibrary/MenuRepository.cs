using ClassLibrary.Models;
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
                    BookAVisitMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;

            }
        }

        // Animals part of menu
        public void ManageAnimalsMenu()
        {
            var animalRepo = new AnimalRepository();
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
                    animalRepo.AddAnimal();
                    break;
                // Go to a menu which contains all the animals, choose which one to update
                case "Update animal":
                    animalRepo.UpdateAnimal();
                    break;
                // Go to a menu which contains all the animals, choose which one to delete
                case "Delete animal":
                    animalRepo.AddAnimal();
                    break;
                case "View animals":
                    animalRepo.AddAnimal();
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

        // Book a visit part of menu

        public void BookAVisitMenu()
        {
            var bookAVisitMenu = AnsiConsole.Prompt(
                      new SelectionPrompt<string>()
                          .Title("[green]Visitor menu[/]")
                          .PageSize(10)
                          .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                          .AddChoices(
                              "Book a visit",
                              "Go back to main menu"
                          ));

            switch (bookAVisitMenu)
            {
                case "Book a visit":
                    BookVisit();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void BookVisit()
        {

        }
    }
}
