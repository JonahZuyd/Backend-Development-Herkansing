using ComponentHotelCL;
using Microsoft.AspNetCore.Mvc;

namespace ComponentHotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReserveringController : Controller
    {
        private readonly DAL dAL;
        public ReserveringController(DAL _dal)
        {
            dAL = _dal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reserveringen = dAL.GetAllReservations();
            return Ok(reserveringen);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservering = dAL.GetReservationById(id);
            if (reservering == null)
            {
                return NotFound();
            }
            return Ok(reservering);
        }

        [HttpPost]
        public IActionResult Create(Reservering reservering)
        {
            dAL.AddReservation(reservering);
            return CreatedAtAction(nameof(GetById), new { id = reservering.ReserveringId }, reservering);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservering reservering)
        {
            if (id != reservering.ReserveringId) return BadRequest();
            dAL.UpdateReservation(reservering);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dAL.DeleteReservation(id);
            return NoContent();
        }


    }
}
