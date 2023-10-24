using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;
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

            using var contest = new ZooContext(options);
                
              
        }
    }
}