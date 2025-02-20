using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectAntivirusBackend.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(string email, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyValue = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyValue))
            {
                throw new InvalidOperationException("JWT Key no está configurada.");
            }
            var key = Encoding.ASCII.GetBytes(keyValue);

            var expireMinutesValue = jwtSettings["ExpireMinutes"];
            if (string.IsNullOrEmpty(expireMinutesValue))
            {
                throw new InvalidOperationException("JWT ExpireMinutes no está configurado.");
            }
            var expireMinutes = double.Parse(expireMinutesValue);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}