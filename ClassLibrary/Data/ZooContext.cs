using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext() : base()
        { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Visit> Visits { get; set; }

        // Tobias Connectionstring: Server=.;Database=BeverlyHillsZoo;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;
        // Julia Connectionstring: Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=Coolbooks;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-P4PT1M9\\SQLEXPRESS;Database=BeverlyHillsZoo;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // TPH
            modelBuilder.Entity<Animal>()
                .HasDiscriminator<string>("AnimalType")
                .HasValue<Water>("Water")
                .HasValue<Land>("Land")
                .HasValue<Air>("Air");

           

            modelBuilder.HasSequence<int>("PassNumber");

            modelBuilder.Entity<Visitor>()
                .Property(p => p.PassNumber)
                .HasDefaultValueSql("NEXT VALUE FOR PassNumber");
        }
        
    }
}
