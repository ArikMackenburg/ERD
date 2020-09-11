using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetOneByIdAsync(int id);
        Task<Room> DeleteAsync(int id);
        Task CreateAsync(Room room);
        Task<bool> UpdateAsync(Room room);
    }
}
