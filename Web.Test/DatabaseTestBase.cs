using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Xunit;

namespace Web.Tests
{
    public abstract class DatabaseTestBase : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly HotelDbContext _db;

        public DatabaseTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new HotelDbContext(
                new DbContextOptionsBuilder<HotelDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel { Name = "Love Shack", StreetAddress = "6969 Doggy Street", City = "Ballplay", State = State.AL, Country = "USA", Phone = "555-000-6969" };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }

       
    }
}