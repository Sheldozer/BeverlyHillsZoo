using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Models.Visit;
using static System.Formats.Asn1.AsnWriter;

namespace ClassLibrary
{
    public class VisitRepository
    {
        private readonly ZooContext _context;

        public VisitRepository(ZooContext context)
        {
            _context = context;
           

        }

        public void AddVisit(int animalId, List<int> visitorIds, DateTime visitDate, Visit.TimeSlot visitTimeSlot)
        {
            var visitorsInVisit = _context.Visitors.Where(v => visitorIds.Contains(v.Id) && v.Removed==false).ToList(); //filter out the removed visitors

            var newVisit = new Visit
            {
                AnimalId = animalId,
                Visitors = visitorsInVisit,
                VisitDate = visitDate,
                VisitTimeSlot = visitTimeSlot
                
            };
            if (!newVisit.IsValid())
            {
                AnsiConsole.MarkupLine("[red]A visit can have a maximum of 5 visitors![/]");
                return;
            }
            
            bool capForAnimalVisit = CheckAnimalsCurrentLimit(animalId, visitDate);

            if ( capForAnimalVisit == true) //if the animal's visits have capped or not
            {
                AnsiConsole.MarkupLine("[red]The animal already has 2 visits this day[/]");
                return;
            }
            else
            {
                var guideRepository = new GuideRepository(_context);
                
                List <Guide> competentGuides = guideRepository.IsGuideCompetent(animalId);
                Guide chosenGuide = guideRepository.IsGuideAvailable(competentGuides, visitDate, visitTimeSlot);
                if (chosenGuide == null)
                {
                    AnsiConsole.MarkupLine("[red]No available guide at the chosen time[/]");
                 
                }
                else
                {
                    newVisit.GuideId = chosenGuide.Id;
                    _context.Add(newVisit);
                    _context.SaveChanges();
                    AnsiConsole.MarkupLine("[green]Visit booked successfully![/]");
                }                
            }         
        }

        public bool CheckAnimalsCurrentLimit(int animalId, DateTime visitDate)
        {
            var currentVisits = _context.Visits
            .Where(v => v.VisitDate == visitDate)
            .Where(v => v.AnimalId == animalId)
            .ToList();

            if (currentVisits.Count == 2)
            {
                //Console.WriteLine("The poor animal has suffered enough for today.");
                return true;
            }
            else
            {
                return false;
            }

        }
        public void DeleteVisit(int toDelete)
        {

            var visitToDelete = _context.Visits.FirstOrDefault(v => v.Id == toDelete);

            if (visitToDelete != null)
            {
                _context.Visits.Remove(visitToDelete);
                _context.SaveChanges();
                Console.WriteLine($"Visit with Id {toDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Visit with Id {toDelete} not found.");
            }

        }

        public void ViewVisits()
        {
            var currentVisitsInfo = _context.Visits
            .Where(v => v.Archived == false)
            .SelectMany(v => v.Visitors, (visit, visitor) => new { visit.Animal.Name, visit.VisitDate, VisitorName = visitor.Name, visit.VisitTimeSlot, visit.Id, visitor.PassNumber, visit.Guide.FirstName})
            .GroupBy(v => v.Id)
            .Select(g => g.ToList())
            .ToList();

            string title = "[yellow1]Current booked visits[/]";
            PrintTitleTable(title);

            var table = new Spectre.Console.Table();
            table.AddColumn("Date");
            table.AddColumn(new TableColumn("Animal").Centered());
            table.AddColumn(new TableColumn("Time slot"));
            table.AddColumn(new TableColumn("Visitor name"));
            table.AddColumn(new TableColumn("Visitor pass number"));
            table.AddColumn(new TableColumn("Guide"));
            table.Centered();

            foreach (var group in currentVisitsInfo)
            {
                table.AddRow(group.First().VisitDate.ToString("d"), group.First().Name, group.First().VisitTimeSlot.ToString(), "[blue]-------[/]");

                foreach (var item in group)
                {
                    table.AddRow("", "", "", item.VisitorName, item.PassNumber.ToString(), item.FirstName);
                }
            }
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Checks database for visits that have passed the date. If yes, turns archived to true.
        /// </summary>
        public void ArchiveOldVisits()
        {
            var visits = _context.Visits.ToList();
            foreach (var visit in visits)
            {
                if (visit.VisitDate.AddDays(1) < DateTime.Now && !visit.Archived)
                {
                    visit.Archived = true;
                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Seeding db with visits
        /// </summary>
        public void SeedVisitsData()
        {
            if (!_context.Visits.Any())
            {
                var animalIds = _context.Animals.Select(a => a.Id).
                    ToList();

                if (animalIds.Count == 0)
                {
                    throw new Exception("Animal Liberation!");
                }

                var guideInfoList = _context.Guides //Collect guides and their competence
                  .Select(g => new {
                      GuideId = g.Id,
                      GuideCompetence = g.GuideCompetence
                  })
              .ToList();

                var animalTypeList = _context.Animals //Collect animal IDs and their habitats
                .Select(a => new {
                    AnimalId = a.Id,
                    HabitatType =
                        (a is Land) ? "Land" :
                        (a is Water) ? "Water" :
                        (a is Air) ? "Air" :
                        "Unknown"
                })
                .ToList();

                var firstAnimal = animalTypeList[0]; //Saving first 4 animals which will have visists
                var secondAnimal = animalTypeList[1];
                var thirdAnimal = animalTypeList[2];
                var fourthAnimal = animalTypeList[3];

                var animals = new List<dynamic> { firstAnimal, secondAnimal, thirdAnimal, fourthAnimal };
                var matchingGuides = new List<dynamic>(); //A list to fill up with matching guides for the 

                foreach (var animal in animals)
                {
                    foreach (var guide in guideInfoList)
                    {
                        if (guide.GuideCompetence.ToString() == animal.HabitatType)
                        {
                            matchingGuides.Add(guide);
                            break;
                        }
                    }
                }

                var visitorIds = _context.Visitors.Where(v => !v.Removed).Select(v => v.Id).ToList();

                var visitorsFirst = _context.Visitors.Where(v => visitorIds.Take(2).Contains(v.Id)).ToList();
                var visitorsSecond = _context.Visitors.Where(v => visitorIds.Skip(1).Take(2).Contains(v.Id)).ToList();
                var visitorsThird = _context.Visitors.Where(v => visitorIds.Take(1).Contains(v.Id)).ToList();
                var visitorFourth = _context.Visitors.Where(v => visitorIds.Skip(1).Take(2).Contains(v.Id)).ToList();

                var visits = new List<Visit>
                {
                    new Visit {AnimalId = firstAnimal.AnimalId, VisitDate = new DateTime(2023, 10, 30), VisitTimeSlot = TimeSlot.Morning, Visitors = visitorsFirst, GuideId = matchingGuides[0].GuideId},
                    new Visit {AnimalId = secondAnimal.AnimalId, VisitDate = new DateTime(2023, 11, 04), VisitTimeSlot = TimeSlot.Afternoon, Archived = false, Visitors = visitorsSecond, GuideId = matchingGuides[1].GuideId},
                    new Visit {AnimalId = thirdAnimal.AnimalId, VisitDate = new DateTime(2023, 10, 03), VisitTimeSlot = TimeSlot.Morning, Archived = true, Visitors = visitorsThird, GuideId = matchingGuides[2].GuideId},
                    new Visit {AnimalId = fourthAnimal.AnimalId, VisitDate = new DateTime(2023, 12, 02), VisitTimeSlot = TimeSlot.Morning, Archived = false, Visitors = visitorFourth, GuideId = matchingGuides[3].GuideId}
                };

                foreach (var visit in visits)
                {
                    _context.Visits.Add(visit);
                }
                _context.SaveChanges();
            }
        }

        public static void PrintTitleTable(string title)
        {
            var titleTable = new Spectre.Console.Table();
            titleTable.AddColumn(title);
            titleTable.Centered();
            AnsiConsole.Write(titleTable);

        }

        public static void ViewArchivedVisits(ZooContext context)
        {
            var oldVisitsInfo = context.Visits
            .Where(v => v.Archived == true)
            .SelectMany(v => v.Visitors, (visit, visitor) => new { visit.Animal.Name, visit.VisitDate, VisitorName = visitor.Name, visit.VisitTimeSlot, visit.Id, visitor.PassNumber, visit.Guide.FirstName })
            .GroupBy(v => v.Id)
            .Select(g => g.ToList())
            .ToList();

            string title = "[yellow1]Archived visits[/]";
            PrintTitleTable(title);

            var table = new Spectre.Console.Table();
            table.AddColumn("Date");
            table.AddColumn(new TableColumn("Animal").Centered());
            table.AddColumn(new TableColumn("Time slot"));
            table.AddColumn(new TableColumn("Visitor name"));
            table.AddColumn(new TableColumn("Visitor pass number"));
            table.AddColumn(new TableColumn("Guide"));
            table.Centered();

            foreach (var group in oldVisitsInfo)
            {
                table.AddRow(group.First().VisitDate.ToString("d"), group.First().Name, group.First().VisitTimeSlot.ToString(), "[blue]-------[/]");

                foreach (var item in group)
                {
                    table.AddRow("", "", "", item.VisitorName, item.PassNumber.ToString(), item.FirstName);
                }
            }
            AnsiConsole.Write(table);

        }

    }
}
