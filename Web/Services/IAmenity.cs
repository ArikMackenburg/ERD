using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IAmenity
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetOneByIdAsync(int id);
        Task<Amenity> DeleteAsync(int id);
        Task CreateAsync(Amenity amenity);
        Task<bool> UpdateAsync(Amenity amenity);
    }
}
