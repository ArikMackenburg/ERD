using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Services
{
    public class DatabaseRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;
        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r=> r.Amenities)
                .ThenInclude(a=> a.Amenity)
                .ToListAsync();
        }

        public async Task<Room> GetOneByIdAsync(int id)
        {
            var hotel = await _context.Rooms.FindAsync(id);
            return hotel;
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExistsAsync(room.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public async Task CreateAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return null;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        private async Task<bool> RoomExistsAsync(int id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }

        public async Task AddAmenityAsync(int roomId, int amenityId)
        {
            var amenity = new RoomAmenity
            {
                RoomId = roomId,
                AmenityId = amenityId
            };

            _context.RoomAmenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmenityAsync(int roomId, int amenityId)
        {
            var amenity = await _context.RoomAmenities.FindAsync(roomId, amenityId);

            _context.RoomAmenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
