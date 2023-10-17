// Beverly Hills Zoo av Tobias J & Julia
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {

        var table = new Table();
        table.BorderColor<Table>(Color.Green);

        table.AddColumn("[yellow1]Welcome to Beverly Hill's Zoo :rolling_on_the_floor_laughing:[/]");
        table.Border(TableBorder.Rounded);
        table.Centered();
        AnsiConsole.Write(table);

        Console.ReadKey();

    }
}