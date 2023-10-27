﻿// <auto-generated />
using System;
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

            modelBuilder.HasSequence<int>("GuideNumber");

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

                    b.ToTable("Animals");

                    b.HasDiscriminator<string>("AnimalType").HasValue("Animal");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ClassLibrary.Models.Guide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuideCompetence")
                        .HasColumnType("int");

                    b.Property<int>("GuideNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR GuideNumber");

                    b.HasKey("Id");

                    b.ToTable("Guides");
                });

            modelBuilder.Entity("ClassLibrary.Models.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<int>("GuideId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VisitTimeSlot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("GuideId");

                    b.ToTable("Visits");
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

                    b.HasKey("Id");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("VisitVisitor", b =>
                {
                    b.Property<int>("VisitorsId")
                        .HasColumnType("int");

                    b.Property<int>("VisitsId")
                        .HasColumnType("int");

                    b.HasKey("VisitorsId", "VisitsId");

                    b.HasIndex("VisitsId");

                    b.ToTable("VisitVisitor");
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

            modelBuilder.Entity("ClassLibrary.Models.Visit", b =>
                {
                    b.HasOne("ClassLibrary.Models.Animal", "Animal")
                        .WithMany("Visits")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLibrary.Models.Guide", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Guide");
                });

            modelBuilder.Entity("VisitVisitor", b =>
                {
                    b.HasOne("ClassLibrary.Models.Visitor", null)
                        .WithMany()
                        .HasForeignKey("VisitorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLibrary.Models.Visit", null)
                        .WithMany()
                        .HasForeignKey("VisitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.Animal", b =>
                {
                    b.Navigation("Visits");
                });
#pragma warning restore 612, 618
        }
    }
}
