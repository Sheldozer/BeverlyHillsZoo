using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverlyHillsZoo.Tests.Visits
{
    public class AddVisit
    {
        [Fact]
        public void AddVisit_ShouldAddVisitToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: "AddVisitTest")
                .Options;

            using var context = new ZooContext(options);
            var visitRepo = new VisitRepository(context);

            var animal = new Land { Id = 1, Name = "Lion", Speed = 30 };
            context.Animals.Add(animal);

            context.Visitors.AddRange(new List<Visitor>
            {
                new Visitor {Id = 1, Name = "John"},
                new Visitor {Id = 2, Name = "Sarah"},
                new Visitor {Id = 3, Name = "Andy"}
            });
            context.SaveChanges();

            var visitorIds = new List<int> { 1, 2, 3 };
            var visitDate = DateTime.Now.AddDays(1);
            var visitTimeSlot = Visit.TimeSlot.Morning;

            // Act
            visitRepo.AddVisit(animal.Id, visitorIds, visitDate, visitTimeSlot);

            //Assert
            context.Visits.Should().ContainSingle();

            var visit = context.Visits.Include(v => v.Visitors).First();
            visit.AnimalId.Should().Be(animal.Id);
            visit.Visitors.Should().HaveCount(3);
            visit.VisitDate.Should().Be(visitDate);
            visit.VisitTimeSlot.Should().Be(visitTimeSlot);
        }
    }
}
