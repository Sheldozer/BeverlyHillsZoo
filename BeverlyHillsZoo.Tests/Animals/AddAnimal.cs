using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BeverlyHillsZoo.Tests.Animals
{
    public class AddAnimal
    {
        [Fact]
        public void AddAnimal_ShouldAddAnimalToTheDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: "AddAnimalTestDatabase")
                .Options;

            using var context = new ZooContext(options);
            var animalRepo = new AnimalRepository(context);

            var animal = new Land { Name = "Lion", Speed=30 };

            // Act
            animalRepo.AddAnimal(animal);

            // Assert
            context.Animals.FirstOrDefault(a => a.Name == "Lion").Should().NotBeNull();
                
              
        }
    }
}