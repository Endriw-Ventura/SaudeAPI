using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoviesAPI.Services
{
    public class JWTService
    {
        private readonly JWTSettings _jwtSettings;

        public JWTService(IOptions<JWTSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GerarToken(string userId, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
