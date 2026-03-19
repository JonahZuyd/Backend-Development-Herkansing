using ComponentHotelCL;
using Microsoft.AspNetCore.Mvc;

namespace ComponentHotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TariefController : Controller
    {
        private readonly DAL dAL;
        public TariefController(DAL _dal)
        {
            dAL = _dal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tarieven = dAL.GetAllRates();
            return Ok(tarieven);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tarief = dAL.GetRateById(id);
            if (tarief == null)
            {
                return NotFound();
            }
            return Ok(tarief);
        }

        [HttpPost]
        public IActionResult Create(Tarief tarief)
        {
            dAL.AddRate(tarief);
            return CreatedAtAction(nameof(GetById), new { id = tarief.TariefId }, tarief);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Tarief tarief)
        {
            if (id != tarief.TariefId) return BadRequest();
            dAL.UpdateRate(tarief);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dAL.DeleteRate(id);
            return NoContent();
        }






    }
}
