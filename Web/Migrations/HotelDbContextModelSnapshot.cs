﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Data;

namespace Web.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    partial class HotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Web.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "WiFi"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Air Conditioning"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Coffee Maker"
                        });
                });

            modelBuilder.Entity("Web.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Ballplay",
                            Country = "USA",
                            Name = "Love Shack",
                            Phone = "555-000-6969",
                            State = 0,
                            StreetAddress = "6969 Doggy Street"
                        },
                        new
                        {
                            Id = 2,
                            City = "Mary's Igloo",
                            Country = "USA",
                            Name = "The Igloo",
                            Phone = "555-000-1111",
                            State = 1,
                            StreetAddress = "0 Degrees Boulevard"
                        },
                        new
                        {
                            Id = 3,
                            City = "Pee Pee Township",
                            Country = "USA",
                            Name = "Down Under",
                            Phone = "555-000-2222",
                            State = 36,
                            StreetAddress = "3 Shakes Lane"
                        });
                });

            modelBuilder.Entity("Web.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 0,
                            Name = "Studio"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 1,
                            Name = "OneBedroom"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 2,
                            Name = "TwoBedroom"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
