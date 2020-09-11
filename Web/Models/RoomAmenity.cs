using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class RoomAmenity
    {
        
        public int RoomId { get; set; }
        
        public int AmenityId { get; set; }
        
        public Room Room { get; set; }
        
        public Amenity Amenity { get; set; }
    }
}
