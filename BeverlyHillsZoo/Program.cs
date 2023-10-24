// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ZooContext>();
            dbContext.Database.Migrate(); // Ensure the database is created and its migrated

            var menuRepository = scope.ServiceProvider.GetRequiredService<MenuRepository>();
            menuRepository.MainMenu();
        }


        using var dbContext = new ZooContext(juliaOptions);

        var visitorRepo = new VisitorRepository(dbContext); // add dependecy injection
        visitorRepo.SeedingVisitorData();
        var animalRepo = new AnimalRepository(dbContext);
        animalRepo.SeedAnimals();
        var visitsRepoSeed = new VisitRepository(dbContext);
        visitsRepoSeed.SeedVisitsData();

        var visitRepo = new VisitRepository(dbContext);
        var guideRepo = new GuideRepository(dbContext);
      

    }
    // Instance of IServiceProvider containing all our configured services, simmilar to the startup project of a web application
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var tobiasConnection = "Server=.;Database=BeverlyHillsZoo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True";
        var juliasConnection = "Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=BeverlyHillsZoo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

        services.AddDbContext<ZooContext>(options => options.UseSqlServer(tobiasConnection));
        // Dependecy injection
        services.AddTransient<AnimalRepository>();
        services.AddTransient<VisitorRepository>();
        services.AddTransient<VisitRepository>();
        services.AddTransient<GuideRepository>();
        services.AddTransient<MenuRepository>();


        return services.BuildServiceProvider();
    }
}