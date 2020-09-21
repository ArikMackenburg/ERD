using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Web.Models.Api
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<HotelRoomDto> Rooms { get; set; }
        
    }
    public class HotelRoomDto
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        [DefaultValue(false)]
        public bool PetFriendly { get; set; }
        //public Hotel Hotel { get; set; }

        public List<string> Amenities  { get; set; }

      
    }
  
    

    
}
