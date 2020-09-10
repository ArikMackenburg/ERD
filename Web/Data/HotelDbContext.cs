using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options) 
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Hotel>()
                .HasData(
                    new Hotel { Id = 1, Name = "Love Shack", StreetAddress = "6969 Doggy Street", City = "Ballplay", State = State.AL, Country = "USA", Phone = "555-000-6969" },
                    new Hotel { Id = 2, Name = "The Igloo", StreetAddress = "0 Degrees Boulevard", City = "Mary's Igloo", State = State.AK, Country = "USA", Phone = "555-000-1111" },
                    new Hotel { Id = 3, Name = "Down Under", StreetAddress = "3 Shakes Lane", City = "Pee Pee Township", State = State.OH, Country = "USA", Phone = "555-000-2222" }
                );

            modelBuilder.Entity<Room>()
                .HasData(
                    new Room { Id = 1, Name = "Studio", Layout = Layout.Studio },
                    new Room { Id = 2, Name = "OneBedroom", Layout = Layout.OneBedroom },
                    new Room { Id = 3, Name = "TwoBedroom", Layout = Layout.TwoBedroom }
                );

            modelBuilder.Entity<Amenity>()
                 .HasData(
                    new Amenity { Id = 1, Name = "WiFi" },
                    new Amenity { Id = 2, Name = "Air Conditioning" },
                    new Amenity { Id = 3, Name = "Coffee Maker" }
                );
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
         
    }
}
