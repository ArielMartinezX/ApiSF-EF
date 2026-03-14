using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiStreetFighter.Controllers
{
    [ApiController]
    [Route("api/SW")]
    public class SWController : Controller
    {
        private readonly IHttpSWProxy _proxy;

        public SWController(IHttpSWProxy proxy)
        {
            _proxy = proxy;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characters = await _proxy.GetAllAsync();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var character = await _proxy.GetByIdAsync(id);
            
            if(character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }
    }
}
