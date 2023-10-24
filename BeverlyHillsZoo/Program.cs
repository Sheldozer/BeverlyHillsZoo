// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        using var dbContext = new ZooContext();


        var visitorRepo = new VisitorRepository(dbContext); // add dependecy injection
        visitorRepo.SeedingVisitorData();
        var animalRepo = new AnimalRepository(dbContext);
        animalRepo.SeedAnimals();
        var visitRepo = new VisitRepository(dbContext);
        var guideRepo = new GuideRepository(dbContext);

        var menuRepository = new MenuRepository(dbContext, animalRepo, visitRepo, visitorRepo, guideRepo);
        menuRepository.MainMenu();
    }
}