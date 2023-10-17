// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
<<<<<<< Updated upstream
=======
        //hej

        var table = new Table();
        table.BorderColor<Table>(Color.Green);

        table.AddColumn("[yellow1]Welcome to Beverly Hill's Zoo[/]");
        table.Border(TableBorder.Rounded);
        table.Centered();

        AnsiConsole.Write(table);

>>>>>>> Stashed changes
        List<Animal> animals = new List<Animal>();

        Animal elephant = new Animal("Elephant");
        Animal eel = new Animal("Eel");
        Animal parrot = new Animal("Parrot");

        animals.Add(elephant);
        animals.Add(eel);
        animals.Add(parrot);

        var menuRepository = new MenuRepository(animals);
        menuRepository.MainMenu();
    }
}