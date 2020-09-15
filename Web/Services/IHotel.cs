using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IHotel
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetOneByIdAsync(int id);
        Task<Hotel> DeleteAsync(int id);
        Task CreateAsync(Hotel hotel);
        Task<bool> UpdateAsync(Hotel hotel);

        Task CreateHotelRoomAsync(HotelRoom hotelRoom);

        Task<IEnumerable<HotelRoom>> GetOneHotelRoomByRoomNumAsync(int hotelId, int roomNumber);
        Task<IEnumerable<HotelRoom>> GetAllHotelRoomsAsync(int hotelId);
        
        Task<HotelRoom> DeleteHotelRoomAsync(int hotelId, int roomNumber);
        Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom);
    }
}
