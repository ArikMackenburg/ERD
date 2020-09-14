using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IHotelRoomRepository
    {
        Task<IEnumerable<HotelRoom>> GetAllHotelRooms(int hotelId);
        Task<HotelRoom> GetOneByIdAsync(int hotelId,int roomNum);
        Task<HotelRoom> DeleteAsync(int hotelId);
        Task CreateAsync(int hotelId, CreateRoom createRoom);
        Task<bool> UpdateAsync(HotelRoom hotelRoom);
       

    }
}
