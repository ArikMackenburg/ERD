using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class HotelRoom
    {
        [Required]
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        [DefaultValue(false)]
        public bool PetFriendly { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        
    
    }
}
