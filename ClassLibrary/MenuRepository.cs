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

        private List<Animal> Animals;
        public MenuRepository(List<Animal> animals)
        {
            Animals = animals;
        }
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
                    AnsiConsole.WriteLine("Go to add animal");
                    break;
                // Go to a menu which contains all the animals, choose which one to update
                case "Update animal":
                    AnsiConsole.WriteLine("Go to update animal");
                    break;
                // Go to a menu which contains all the animals, choose which one to delete
                case "Delete animal":
                    DeleteAnimal();
                    break;
                case "View animals":
                    ViewAnimals();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void DeleteAnimal()
        {

            var deleteMenu = AnsiConsole.Prompt(
                new SelectionPrompt<Animal>()
                .Title("[green]Select an animal to delete[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more animals)[/]")
                .AddChoices(Animals));

            Animals.Remove(deleteMenu);
            ManageAnimals();
        }

        public void ViewAnimals()
        {
            var animalTable = new Table();

            animalTable.AddColumn("Animal Name");

            foreach (var animal in Animals)
            {
                animalTable.AddRow(animal.Name);
            }

            AnsiConsole.Render(animalTable);

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadLine();
            ManageAnimals();
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
                    AddVisitor();
                    break;
                case "Update visitor":
                    UpdateVisitor();
                    break;
                case "Delete visitor":
                    DeleteVisitor();
                    break;
                case "View visitors":
                    ViewVisitors();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void AddVisitor()
        {

        }

        public void UpdateVisitor()
        {

        }

        public void DeleteVisitor() 
        { 

        }

        public void ViewVisitors()
        {

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
                    AddGuide();
                    break;
                case "Update guide":
                    UpdateGuide();
                    break;
                case "Delete guide":
                    DeleteGuide();
                    break;
                case "View guides":
                    ViewGuides();
                    break;
                case "Go back to main menu":
                    MainMenu();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public void AddGuide()
        {

        }
        
        public void UpdateGuide()
        {

        }

        public void DeleteGuide()
        {

        }

        public void ViewGuides()
        {

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
