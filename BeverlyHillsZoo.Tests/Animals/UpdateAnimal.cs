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

namespace BeverlyHillsZoo.Tests.Animals
{
    public class UpdateAnimal
    {
        [Fact]
        public void UpdateAnimal_ShouldUpdateAnimalInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: "UpdateAnimalTest")
                .Options;

            using var context = new ZooContext(options);
            var animalRepo = new AnimalRepository(context);

            var waterAnimal = new Water { Id = 1, Name = "Seel", DivingDepth = 100 };
            context.Animals.Add(waterAnimal);
            context.SaveChanges();

            // new values
            waterAnimal.DivingDepth = 150;

            // Act
            animalRepo.UpdateAnimal(waterAnimal);

            // Assert

            var updatedAnimal = context.Animals.FirstOrDefault(a => a.Name == "Seel") as Water;
            updatedAnimal.Should().NotBeNull();
            updatedAnimal.DivingDepth.Should().Be(150);
        }
    }
}