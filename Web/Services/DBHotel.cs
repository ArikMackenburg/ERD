using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;


namespace Web.Services
{
    public class DBHotel : IHotel
    {
        private readonly HotelDbContext _context;
        public DBHotel(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                .Include(h=> h.Rooms)
                .ThenInclude(r=> r.Room)
                .ThenInclude(r=> r.Amenities)
                .ThenInclude(a=> a.Amenity)
                .ToListAsync();
        }
        public async Task<Hotel> GetOneByIdAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel;
        }
        public async Task<bool> UpdateAsync(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExistsAsync(hotel.Id))
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
        public async Task CreateAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task<Hotel> DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return null;
            }
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
        private async Task<bool> HotelExistsAsync(int id)
        {
            return await _context.Hotels.AnyAsync(e => e.Id == id);
        }
        public async Task CreateHotelRoomAsync(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<HotelRoom>> GetOneHotelRoomByRoomNumAsync(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId)
                .Where(hr => hr.RoomNumber == roomNumber)
                .Include(hr => hr.Room)
                .ThenInclude(r=> r.Amenities)
                .ThenInclude(a=> a.Amenity)
                .ToListAsync();
            return hotelRoom;
        }
        public async Task<IEnumerable<Hotel>> GetAllHotelRoomsAsync(int hotelId)
        {
            var hotelRoom = await _context.Hotels
                .Where(h => h.Id == hotelId)
                .Include(h=> h.Rooms)
                .ThenInclude(hr=> hr.Room)
                .ThenInclude(r => r.Amenities)
                .ThenInclude(a => a.Amenity)
                .ToListAsync();
            return hotelRoom;
        }
        private async Task<bool> HotelRoomExistsAsync(int hotelId, int roomNumber)
        {
            return await _context.HotelRooms.AnyAsync(e => e.HotelId == hotelId && e.RoomNumber == roomNumber);
        }
        public async Task<HotelRoom> DeleteHotelRoomAsync(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(hotelId,roomNumber);
            if (hotelRoom == null)
            {
                return null;
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoom;
        }
        public async Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelRoomExistsAsync(hotelRoom.HotelId,hotelRoom.RoomNumber))
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
    }
}
