using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Services;
using Newtonsoft.Json;

namespace Web.Controllers
{
    [Route("api/Hotels")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomRepository repository;
        public HotelRoomsController(IHotelRoomRepository repository, HotelDbContext context)
        {
            this.repository = repository;
        }

        // GET: api/Hotels/1
        [HttpGet("{hotelId}/Rooms")]
        public async Task<IEnumerable<HotelRoom>> GetHotelRooms(int hotelId)
        {
            return await repository.GetAllHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/Rooms/{roomNum}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNum)
        {
            return await repository.GetOneByIdAsync(hotelId, roomNum);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.HotelId)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(hotelRoom);

            if (didUpdate == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPost("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, [FromBody] CreateRoom hotelRoom)
        {
            await repository.CreateAsync(hotelId, hotelRoom);
           

            return CreatedAtAction("GetHotelRoom", new { id = hotelId }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int id)
        {
            var hotel = await repository.DeleteAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

       
    }
}
