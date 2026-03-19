using ComponentHotelCL;
using Microsoft.AspNetCore.Mvc;

namespace ComponentHotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelkamerController : Controller
    {
        private readonly DAL dAL;
        public HotelkamerController(DAL _dal)
        {
            dAL = _dal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var hotelkamers = dAL.GetAllHotelRooms();
            return Ok(hotelkamers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotelkamer = dAL.GetHotelRoomById(id);
            if (hotelkamer == null)
            {
                return NotFound();
            }
            return Ok(hotelkamer);
        }

        [HttpPost]
        public IActionResult Create(Hotelkamer hotelkamer)
        {
            dAL.AddHotelRoom(hotelkamer);
            return CreatedAtAction(nameof(GetById), new { id = hotelkamer.HotelkamerId }, hotelkamer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Hotelkamer hotelkamer)
        {
            if (id != hotelkamer.HotelkamerId) return BadRequest();
            dAL.UpdateHotelRoom(hotelkamer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dAL.DeleteHotelRoom(id);
            return NoContent();
        }


    }
}
