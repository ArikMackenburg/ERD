using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Api;

namespace Web.Services
{
    public interface IHotel
    {
        IEnumerable<HotelDto> GetAllAsync();
        HotelDto GetOneByIdAsync(int id);
        Task<Hotel> DeleteAsync(int id);
        Task CreateAsync(Hotel hotel);
        Task<bool> UpdateAsync(Hotel hotel);

        Task CreateHotelRoomAsync(HotelRoom hotelRoom);

        IEnumerable<HotelRoomDto> GetOneHotelRoomByRoomNumAsync(int hotelId, int roomNumber);
        IEnumerable<HotelRoomDto> GetAllHotelRoomsAsync(int hotelId);
        
        Task<HotelRoom> DeleteHotelRoomAsync(int hotelId, int roomNumber);
        Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom);
    }
}
