﻿// <auto-generated />
using ClassLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClassLibrary.Migrations
{
    [DbContext(typeof(ZooContext))]
    partial class ZooContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("PassNumber");

            modelBuilder.Entity("ClassLibrary.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnimalType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animals", (string)null);

                    b.HasDiscriminator<string>("AnimalType").HasValue("Animal");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ClassLibrary.Models.Guide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Guides", (string)null);
                });

            modelBuilder.Entity("ClassLibrary.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR PassNumber");

                    b.Property<bool>("Removed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Visitors", (string)null);
                });

            modelBuilder.Entity("ClassLibrary.Models.Air", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.Animal");

                    b.Property<int>("MaxAltitude")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Air");
                });

            modelBuilder.Entity("ClassLibrary.Models.Land", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.Animal");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Land");
                });

            modelBuilder.Entity("ClassLibrary.Models.Water", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.Animal");

                    b.Property<int>("DivingDepth")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Water");
                });
#pragma warning restore 612, 618
        }
    }
}
