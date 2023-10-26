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
        public ZooContext(DbContextOptions<ZooContext> options)
         : base(options)
        { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Visit> Visits { get; set; }

       
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

            modelBuilder.HasSequence<int>("GuideNumber");

            modelBuilder.Entity<Guide>()
                .Property(g => g.GuideNumber)
                .HasDefaultValueSql("NEXT VALUE FOR GuideNumber");
        
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Guide) // Configure the relationship between Visit and Guide
                .WithMany()
                .HasForeignKey(v => v.GuideId);
        
    }
        
    }
}
