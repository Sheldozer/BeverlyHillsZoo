using ClassLibrary;
using ClassLibrary.Data;
using Moq;

namespace BeverlyHillsZoo.Tests.Animals
{
    public class AddAnimal
    {
        [Fact]
        public void AddAnimal_ShouldAddAnimalToTheDatabase()
        {
            // Arrange
            var mockContext = new Mock<ZooContext>();
            var animalRepo = new AnimalRepository(mockContext.Object);
        }
    }
}