﻿// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using ClassLibrary.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var updateVisitsStatus = new VisitRepository();
            updateVisitsStatus.ArchiveOldVisits();

        var visitorSeeding = new VisitorRepository();
        visitorSeeding.SeedingVisitorData();
        var animalSeeding = new AnimalRepository();
        animalSeeding.SeedAnimals();

        var menuRepository = new MenuRepository();
        menuRepository.MainMenu();
    }
}