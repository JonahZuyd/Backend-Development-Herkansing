using ComponentHotelCL;
using Microsoft.AspNetCore.Mvc;

namespace ComponentHotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastController : Controller
    {
        private readonly DAL dAL;
        public GastController(DAL _dal)
        {
            dAL = _dal;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var gasten = dAL.GetAllGuests();
            return Ok(gasten);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gast = dAL.GetGuestById(id);
            if (gast == null)
            {
                return NotFound();
            }
            return Ok(gast);
        }

        [HttpPost]
        public IActionResult Create(Gast gast)
        {
            dAL.AddGuest(gast);
            return CreatedAtAction(nameof(GetById), new { id = gast.GastId }, gast);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Gast gast)
        {
            if (id != gast.GastId) return BadRequest();
            dAL.UpdateGuest(gast);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dAL.DeleteGuest(id);
            return NoContent();
        }



    }
}
