﻿// Beverly Hills Zoo av Tobias J & Julia
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

            var animalRepo = scope.ServiceProvider.GetRequiredService<AnimalRepository>();
            animalRepo.SeedAnimals();

            var guideRepo = scope.ServiceProvider.GetService<GuideRepository>();
            guideRepo.SeedGuides();

            var visitorRepo = scope.ServiceProvider.GetRequiredService<VisitorRepository>();
            visitorRepo.SeedingVisitorData();

            var visitsRepo = scope.ServiceProvider.GetRequiredService<VisitRepository>();
            visitsRepo.SeedVisitsData();

            visitsRepo.ArchiveOldVisits();

            var menuManager = scope.ServiceProvider.GetRequiredService<MenuManager>();
            menuManager.MainMenu();

        }


        //var visitRepo = new VisitRepository(dbContext);
        //var guideRepo = new GuideRepository(dbContext);

    }
    // Instance of IServiceProvider containing all our configured services, simmilar to the startup project of a web application
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var tobiasConnection = "Server=.;Database=BeverlyHillsZoo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True";
        var juliasConnection = "Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=BeverlyHillsZoo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

        services.AddDbContext<ZooContext>(options => options.UseSqlServer(juliasConnection));
        // Dependecy injection
        services.AddTransient<AnimalRepository>();
        services.AddTransient<VisitorRepository>();
        services.AddTransient<VisitRepository>();
        services.AddTransient<GuideRepository>();
        services.AddTransient<MenuManager>();

        return services.BuildServiceProvider();
    }
}