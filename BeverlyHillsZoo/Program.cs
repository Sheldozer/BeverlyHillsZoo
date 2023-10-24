// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {

        var tobiasOptions = new DbContextOptionsBuilder<ZooContext>()
            .UseSqlServer("Server=.;Database=BeverlyHillsZoo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;")
            .Options;

        var juliaOptions = new DbContextOptionsBuilder<ZooContext>()
          .UseSqlServer("Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=BeverlyHillsZoo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;")
          .Options;

        using var dbContext = new ZooContext(juliaOptions);

        var visitorRepo = new VisitorRepository(dbContext); // add dependecy injection
        visitorRepo.SeedingVisitorData();
        var animalRepo = new AnimalRepository(dbContext);
        animalRepo.SeedAnimals();
        var visitsRepoSeed = new VisitRepository(dbContext);
        visitsRepoSeed.SeedVisitsData();

        var visitRepo = new VisitRepository(dbContext);
        var guideRepo = new GuideRepository(dbContext);
      


        var menuRepository = new MenuRepository(dbContext, animalRepo, visitRepo, visitorRepo, guideRepo);
        menuRepository.MainMenu();
    }
}