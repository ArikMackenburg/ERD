using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetOneByIdAsync(int id);
        Task<Hotel> DeleteAsync(int id);
        Task CreateAsync(Hotel hotel);
        Task<bool> UpdateAsync(Hotel hotel);
       
    }
}
