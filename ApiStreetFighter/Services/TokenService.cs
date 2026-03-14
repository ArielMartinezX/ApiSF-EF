using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiStreetFighter.Services
{
    public class TokenService
    {
        private readonly string _key;

        public TokenService(IConfiguration config)
        {
            _key = config["JwtKey"];
        }

        public string CreateToken(string userName)
        {
            //crear claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };
            //hashear key
            var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_key)
                );
            //Credenciales
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //armar el token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
