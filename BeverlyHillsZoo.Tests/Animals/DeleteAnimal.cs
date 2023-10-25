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
    public class DeleteAnimal
    {
        [Fact]
        public void DeleteAnimal_ShouldDeleteAnimalFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAnimalTest")
                .Options;

            using var context = new ZooContext(options);
            var animalRepo = new AnimalRepository(context);

            var animal = new Land { Id = 1, Name = "Lion", Speed = 100 };
            context.Animals.Add(animal);
            context.SaveChanges();

            // Act
            animalRepo.DeleteAnimal(animal);

            // Assert
            context.Animals.FirstOrDefault(a => a.Name == "Lion").Should().BeNull();
        }
    }
}
