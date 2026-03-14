using ApiStreetFighter.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiStreetFighter.Controllers
{
    [ApiController]
    [Route("api/fighter")]
    public class UserController : Controller
    {
        private readonly TokenService _tokenService;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username != "admin" || password != "1234")
            {
                return Unauthorized();
            }
            
            var token = _tokenService.CreateToken(username);
            
            return Ok(token);
        }
    }
}
