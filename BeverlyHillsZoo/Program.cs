// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using ClassLibrary.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var menuRepository = new MenuRepository();
        menuRepository.MainMenu();
    }
}