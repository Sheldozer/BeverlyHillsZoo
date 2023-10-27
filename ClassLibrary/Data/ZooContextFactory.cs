using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    // Factory for creating instances of ZooContext during design
    public class ZooContextFactory : IDesignTimeDbContextFactory<ZooContext>
    {
        public ZooContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ZooContext>();

            var tobiasConnection = "Server=.;Database=BeverlyHillsZoo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True";
            var juliasConnection = "Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=BeverlyHillsZoo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

            builder.UseSqlServer(juliasConnection);

            return new ZooContext(builder.Options);
        }
    }
}
