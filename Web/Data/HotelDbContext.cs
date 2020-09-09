using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options) 
            : base(options)
        {
            
        }
    }
}
