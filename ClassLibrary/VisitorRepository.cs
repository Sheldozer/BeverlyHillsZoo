using ClassLibrary.Data;
using ClassLibrary.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary
{
    public class VisitorRepository
    {
        private ZooContext _dbContext;

        public VisitorRepository(ZooContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddVisitor()
        {
            string input = UserInputVisitorName();
            var newVisitor = new Visitor
            {
                Name = input,
            };

            _dbContext.Visitors.Add(newVisitor);
            _dbContext.SaveChanges();
        }
        public void UpdateVisitor()
        {
            int input = UserInputVisitorPassNumber();

            if (input == -1)
            {
                return;
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow1]Update visitor's name[/]");

                    string newName = UserInputVisitorName();

                    var qry = _dbContext.Visitors.First(v => v.PassNumber == input);
                    qry.Name = newName;

                    _dbContext.SaveChanges();
            }            
        }
        public void DeleteVisitor()
        {
            AnsiConsole.MarkupLine("[red]Delete visitor[/]");

            int input = UserInputVisitorPassNumber();

            if (input == -1)
            {
                
                return; 
            }
            else
            {
                var qry = _dbContext.Visitors.FirstOrDefault(v => v.PassNumber == input);
                    qry.Removed = true;
                    _dbContext.SaveChanges();
            }
        }
        public void ViewVisitors()
        {
            List<(string Name, int PassNumber)> visitorInfoList = _dbContext.Visitors
            .Where(visitor => visitor.Removed == false)
            .Select(visitor => new { visitor.Name, visitor.PassNumber })
            .ToList()
            .Select(visitor => (visitor.Name, visitor.PassNumber))
            .ToList();

            var table = new Table();

            table.AddColumn("Name");
            table.AddColumn(new TableColumn("Pass Number").Centered());

            foreach (var visitor in visitorInfoList)
            {
                table.AddRow(visitor.Name, visitor.PassNumber.ToString());
            }

            AnsiConsole.Render(table);

        }
        public int UserInputVisitorPassNumber()
        {
            Console.WriteLine("Please enter visitor's pass number");

            int inputToInt;
            string input = Console.ReadLine();
            bool isParsable = int.TryParse(input, out  inputToInt); //Is it even digits? If yes, save digits in variable

            var deletedUser = _dbContext.Visitors.FirstOrDefault(v => v.PassNumber == inputToInt);

            if (!isParsable && deletedUser.Removed == true) //If the input is not digits or if visitor is removed in db
            {
                Console.WriteLine("No such active visitor");
                 return -1;
            }
            else
            {
               return int.Parse(input);
            }   
        }
        public string UserInputVisitorName()
        {
            Console.WriteLine("Enter name of visitor");
            return Console.ReadLine();
        }

        public void SeedingVisitorData()
        {
            if (!_dbContext.Visitors.Any())
            {
                var visitors = new List<Visitor>
            {
                    new Visitor { Name = "Karmilla Giovanni", Removed = false },
                    new Visitor { Name = "Mr Clean", Removed = false },
                    new Visitor { Name = "Dana Scully", Removed = false },
                    new Visitor { Name = "Fox Mulder", Removed = true },

            };

                foreach (var visitor in visitors)
                {
                    _dbContext.Visitors.Add(visitor);
                }

                _dbContext.SaveChanges();
            }

                
        }
    }
}
