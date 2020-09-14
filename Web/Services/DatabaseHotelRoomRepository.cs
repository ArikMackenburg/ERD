using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Newtonsoft.Json;

namespace Web.Services
{
    public class DatabaseHotelRoomRepository: IHotelRoomRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

      

        public Task<HotelRoom> DeleteAsync(int hotelId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HotelRoom>> GetAllHotelRooms(int hotelId)
        {
            return await _context.HotelRooms
                .Include(r => r.Hotel)
                .Where(h => h.HotelId == hotelId)
                .ToListAsync();
            
        }

        public async Task<HotelRoom> GetOneByIdAsync(int hotelId,int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(roomNumber);
            return hotelRoom;
        }

        public Task<bool> UpdateAsync(HotelRoom hotelRoom)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(int hotelId, CreateRoom createRoom)
        {
            var room = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = createRoom.RoomNumber,
                RoomId = createRoom.RoomId
            };
            _context.HotelRooms.Add(room);
            await _context.SaveChangesAsync();
        }

        private bool HotelRoomExists(int id)
        {
            return _context.HotelRooms.Any(e => e.HotelId == id);
        }
    }
}
