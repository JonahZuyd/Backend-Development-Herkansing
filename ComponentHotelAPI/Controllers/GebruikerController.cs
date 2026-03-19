using ComponentHotelCL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ComponentHotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GebruikerController : Controller
    {
        private readonly DAL dAL;
        public GebruikerController(DAL _dal)
        {
            dAL = _dal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var gebruikers = dAL.GetAllUsers();
            return Ok(gebruikers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gebruiker = dAL.GetUserById(id);
            if (gebruiker == null)
            {
                return NotFound();
            }
            return Ok(gebruiker);
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var gebruiker = new Gebruiker
            {
                GastId = dto.GastId,
                WachtwoordHash = dto.WachtwoordHash,
                Rol = dto.Rol
            };
            dAL.AddUser(gebruiker);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerId) return BadRequest();
            dAL.UpdateUser(gebruiker);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dAL.DeleteUser(id);
            return NoContent();
        }

    }
}
