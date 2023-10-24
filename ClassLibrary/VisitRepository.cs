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
            var visitorsInVisit = _context.Visitors.Where(v => visitorIds.Contains(v.Id)).ToList();

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

            _context.Add(newVisit);
            _context.SaveChanges();
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
            .SelectMany(v => v.Visitors, (visit, visitor) => new { visit.Animal.Name, visit.VisitDate, VisitorName = visitor.Name, visit.VisitTimeSlot, visit.Id, visitor.PassNumber })
            .GroupBy(v => v.Id)
            .Select(g => g.ToList())
            .ToList();

            var titleTable = new Spectre.Console.Table();
            titleTable.AddColumn("[yellow1]Current booked visits[/]");
            titleTable.Centered();
            AnsiConsole.Write(titleTable);

            var table = new Spectre.Console.Table();
            table.AddColumn("Date");
            table.AddColumn(new TableColumn("Animal").Centered());
            table.AddColumn(new TableColumn("Time slot"));
            table.AddColumn(new TableColumn("Visitor name"));
            table.AddColumn(new TableColumn("Visitor pass number"));
            table.Centered();

            foreach (var group in currentVisitsInfo)
            {
                table.AddRow(group.First().VisitDate.ToString(), group.First().Name, group.First().VisitTimeSlot.ToString(), "[blue]-------[/]");

                foreach (var item in group)
                {
                    table.AddRow("", "", "", item.VisitorName, item.PassNumber.ToString());
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
        /// Seeding the visits but not visitors attached to it
        /// </summary>
        public void SeedVisitsData()
        {
            if (!_context.Visits.Any())
            {
                List<int> visitorIds = new List<int> { 1, 2 };
                List<Visitor> visitorsFirst = _context.Visitors.Where(v => visitorIds.Contains(v.Id)).ToList();

                visitorIds = new List<int> { 3, 1 };
                List<Visitor> visitorsSecond = _context.Visitors.Where(v => visitorIds.Contains(v.Id)).ToList();

                visitorIds = new List<int> { 1 };
                List<Visitor> visitorsThird = _context.Visitors.Where(v => visitorIds.Contains(v.Id)).ToList();

                var visits = new List<Visit>
            {
                    new Visit {AnimalId = 1, VisitDate = new DateTime(2023, 10, 30), VisitTimeSlot = TimeSlot.Morning, Archived = false, Visitors = visitorsFirst},
                    new Visit {AnimalId = 2, VisitDate = new DateTime(2023, 11, 04), VisitTimeSlot = TimeSlot.Afternoon, Archived = false,Visitors = visitorsSecond},
                    new Visit {AnimalId = 3, VisitDate = new DateTime(2023, 10, 03), VisitTimeSlot = TimeSlot.Morning, Archived = true, Visitors = visitorsThird},
            };

                foreach (var visit in visits)
                {
                    _context.Visits.Add(visit);
                }
                _context.SaveChanges();
            }
        }
       
    }
}
