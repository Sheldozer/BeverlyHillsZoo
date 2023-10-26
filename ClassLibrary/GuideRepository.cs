using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Models.Guide;
using static ClassLibrary.Models.Visit;

namespace ClassLibrary
{
    public class GuideRepository
    {
        private readonly ZooContext _context;
        public GuideRepository(ZooContext context)
        {
            _context = context;
        }
        public void AddGuide()
        {

            string newGuideName = UserInputsGuideNameUpdate();
            Competence newCompetence = UserSelectionGuideCompetenceUpdate();

            _context.Guides.Add(new Guide
            {
                FirstName = newGuideName,
                GuideCompetence = newCompetence
            });
            _context.SaveChanges();
        }

        public void UpdateGuide()
        {
            AnsiConsole.MarkupLine("[yellow]Update guide, enter -1 to return to menu[/]");

            int input = UserInputGuideNumber();

            bool isGuideinDatabase = IsGuideInDatabase(input);
            if (isGuideinDatabase)
            {
                string updatedGuideName = UserInputsGuideNameUpdate();

                Competence chosenCompetence = UserSelectionGuideCompetenceUpdate();


                var qry = _context.Guides.First(g => g.GuideNumber == input);
                qry.FirstName = updatedGuideName;
                qry.GuideCompetence = chosenCompetence;

                _context.SaveChanges();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No guide matches the guide number[/]");

                return;
            }


        }
        /// <summary>
        /// Deletes a guide completely from the database. All booked visits connected to the guide are being removed.
        /// </summary>
        public void DeleteGuide()
        {
            AnsiConsole.MarkupLine("[red]Delete guide, enter -1 to return to menu[/]");

            int input = UserInputGuideNumber();

            if (input == -1)
            {

                return;
            }
            else
            {
                var guideToDelete = _context.Guides.FirstOrDefault(g => g.GuideNumber == input);

                if (guideToDelete != null)
                {
                    _context.Guides.Remove(guideToDelete);
                    _context.SaveChanges();
                    AnsiConsole.MarkupLine($"Guide with GuideNumber {input} has been deleted.");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No guide found with the provided GuideNumber.[/]");
                }
            }

        }

        public void ViewGuides()
        {
            var guideInfoList = _context.Guides
                .Select(guide => new
                {
                    guide.FirstName,
                    GuideNumber = guide.GuideNumber.ToString(),
                    GuideCompetence = guide.GuideCompetence.ToString()
                })
            .ToList();

            //foreach (var guide in guideInfoList)
            //{
            //    Console.WriteLine(guide.FirstName + " " + guide.GuideNumber + " " + guide.GuideCompetence);
            //}

            var table = new Spectre.Console.Table();
            table.AddColumn("First Name");
            table.AddColumn("Guide Number");
            table.AddColumn("Guide Competence");

            foreach (var guide in guideInfoList)
            {
                table.AddRow(guide.FirstName, guide.GuideNumber, guide.GuideCompetence);
            }

            AnsiConsole.Write(table);

        }

        public void SeedGuides()
        {
            if (!_context.Guides.Any())
            {

                List<Guide> guides = new List<Guide>
                {
                    new Guide { FirstName = "Doctor Jones", GuideCompetence = Guide.Competence.Land},
                    new Guide { FirstName = "Professor Alan Grant", GuideCompetence = Guide.Competence.Air},
                    new Guide { FirstName = "Captain Nemo", GuideCompetence = Guide.Competence.Water},
                    new Guide { FirstName = "Doctor Beth Smith", GuideCompetence = Guide.Competence.Land}
                };

                foreach (var guide in guides)
                {
                    _context.Guides.Add(guide);
                }
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Gets us a list of guides with the right competence for the chosen animal
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns>List of suitable guides</returns>
        public List<Guide> IsGuideCompetent(int animalId)
        {
            Competence neededCompentence;

            var animalType = _context.Animals //Find out the habitat of chosen animal
                .Where(a => a.Id == animalId)
                .Select(a =>
                    a is Land ? "1" :
                    a is Water ? "2" :
                    a is Air ? "0" :
                    "Unknown"
                )
                .FirstOrDefault();

            int typeID = Convert.ToInt32(animalType); //create exception if parsing fails?

            if (typeID == 0)
            {
                neededCompentence = Competence.Air;
            }
            else if (typeID == 1)
            {
                neededCompentence = Competence.Land;
            }
            else
            {
                neededCompentence = Competence.Water;
            }

            return _context.Guides.Where(g => g.GuideCompetence == neededCompentence).ToList();
        }
        /// <summary>
        /// Picks the first competent and availbable guide.
        /// </summary>
        /// <param name="competentGuides"></param>
        /// <param name="visitDate"></param>
        /// <param name="timeSlot"></param>
        /// <returns>a guide object</returns>
        public Guide IsGuideAvailable(List<Guide> competentGuides, DateTime visitDate, TimeSlot timeSlot)
        {
            Guide chosenGuide = null;

            foreach (var guide in competentGuides)
            {
                var availableGuides = _context.Visits
                    .Where(v => v.GuideId == guide.Id) //current guide
                    .Where(v => v.VisitDate == visitDate)
                    .Where(v => v.VisitTimeSlot == timeSlot)
                    .ToList();

                if (availableGuides.Count == 0)
                {
                    //Sets the guide for the visit if they're not booked that day (at all)
                    chosenGuide = guide;
                    break;
                }
                else if (availableGuides.Any(g => g.VisitTimeSlot != timeSlot))
                {
                    //Sets the guide for the visit if they're not booked on that time slot already
                    chosenGuide = guide;
                    break;
                }
            }
            return chosenGuide;


        }

        public int UserInputGuideNumber()
        {
            Console.WriteLine("Please enter guide number");

            int inputToInt;
            string input = Console.ReadLine();
            bool isParsable = int.TryParse(input, out inputToInt); //Is it even digits? If yes, save digits in variable

            var deletedGuide = _context.Guides.FirstOrDefault(g => g.GuideNumber == inputToInt);

            if (!isParsable && deletedGuide == null) //If the input is not digits or if the guide is not even in the db
            {
                Console.WriteLine("No such guide found in system");
                return -1;
            }
            else
            {
                return int.Parse(input);
            }
        }
        /// <summary>
        /// Asks user for new guide name and competence
        /// </summary>
        /// <returns>list of two strings</returns>
        public string UserInputsGuideNameUpdate()
        {
            Console.WriteLine("Enter name of guide");
            string newGuideName = Console.ReadLine();


            return newGuideName;
        }

        public Competence UserSelectionGuideCompetenceUpdate()
        {
            var chooseCompetence = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose competence")
                .PageSize(3)
                .AddChoices("Air", "Land", "Water")
                .UseConverter(competence => competence.ToString()));

            Competence chosenCompetence;

            if (chooseCompetence == "Air")
            {
                chosenCompetence = Competence.Air;
            }
            else if (chooseCompetence == "Land")
            {
                chosenCompetence = Competence.Land;
            }
            else
            {
                chosenCompetence = Competence.Water;
            }


            return chosenCompetence;
        }

        public bool IsGuideInDatabase(int input)
        {
            var currentGuides = _context.Guides.FirstOrDefault(g => g.GuideNumber == input);

            if (currentGuides == null)
                return false;
            else
                return true;
        }
    }
}
