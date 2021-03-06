﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel repository;

        public HotelsController(IHotel repository, HotelDbContext context)
        {
            this.repository = repository;
            
        }
        // GET: api/Hotels
        [HttpGet]
        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await repository.GetAllAsync();
            
        }
        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            return await repository.GetOneByIdAsync(id);
        }
        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(hotel);

            if (didUpdate == false)
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await repository.CreateAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            var hotel = await repository.DeleteAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
        // POST api/Hotels/1/Rooms
        [HttpPost("{hotelId}/Rooms")]

        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            await repository.CreateHotelRoomAsync(hotelRoom);
            return CreatedAtAction("GetHotelRoom", new { hotelId = hotelRoom.HotelId, roomNumber = hotelRoom.RoomNumber }, hotelRoom);
        }
        // GET api/Hotels/1/Rooms/69
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]

        public async Task<IEnumerable<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await repository.GetOneHotelRoomByRoomNumAsync( hotelId, roomNumber);
        }
        // GET api/Hotels/1/Rooms
        [HttpGet("{hotelId}/Rooms")]
        public async Task<IEnumerable<Hotel>> GetAllHotelRooms(int hotelId)
        {
            return await repository.GetAllHotelRoomsAsync(hotelId);
        }
        // DELETE: api/Hotels/1/Rooms/69
        [HttpDelete("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await repository.DeleteHotelRoomAsync(hotelId,roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }
            return hotelRoom;
        }
    }
}
