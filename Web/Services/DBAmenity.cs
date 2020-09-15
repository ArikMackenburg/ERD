using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;


namespace Web.Services
{
    public class DBAmenity : IAmenity
    {
        private readonly HotelDbContext _context;
        public DBAmenity(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenities
                .Include(a=> a.Rooms)
                .ThenInclude(r=> r.Room)
                .ThenInclude(r=> r.Amenities)
                .ToListAsync();
        }

        public async Task<Amenity> GetOneByIdAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            return amenity;
        }

        public async Task<bool> UpdateAsync(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AmenityExistsAsync(amenity.Id))
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
        public async Task CreateAsync(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<Amenity> DeleteAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity == null)
            {
                return null;
            }

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return amenity;
        }

        private async Task<bool> AmenityExistsAsync(int id)
        {
            return await _context.Amenities.AnyAsync(e => e.Id == id);
        }
    }
}
