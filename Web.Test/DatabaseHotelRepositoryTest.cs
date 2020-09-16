using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using Web.Tests;
using Xunit;

namespace Web.Test
{
    public class DatabaseHotelRepositoryTest : DatabaseTestBase
    {
        [Fact(Skip = "It's not ready yet")]
        public async void CanGetHotelsWithRooms()
        {
            var hotel = await CreateAndSaveTestHotel();
            var rooms = await CreateAndSaveTestRoom();
            var hotelRoom = await CreateAndSaveTestHotelRoom();

            _db.HotelRooms.Add(new HotelRoom
            {
                HotelId = 1,
                RoomNumber = 69,
                RoomId = 1,
                PetFriendly = true,
                Rate = 69.00m
            });

            _db.Hotels.Add(new Hotel
            {
                Name = "Love Shack",
                StreetAddress = "6969 Doggy Street",
                City = "Ballplay",
                State = State.AL,
                Country = "USA",
                Phone = "555-000-6969"
            });

            await _db.SaveChangesAsync();

            var repository = new DBHotel(_db);

            var dto = repository.GetOneByIdAsync(hotel.Id);

            Assert.NotNull(dto);

            
        }
    }
}
