// Beverly Hills Zoo av Tobias J & Julia
using ClassLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        //hej
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