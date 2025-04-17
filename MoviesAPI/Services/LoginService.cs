using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Login;
using MoviesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace MoviesAPI.Services
{
    public class LoginService
    {
        private readonly APIContext _context;
        private readonly JWTSettings _jwtSettings;

        public LoginService(APIContext context, IOptions<JWTSettings> jwtOptions)
        {
            _context = context;
            _jwtSettings = jwtOptions.Value;
        }

        public LoggedUserDTO? Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user != null)
            {
                return CreateLoggedUserDTO(user.Id, user.Email, "user");
            }

            var doctor = _context.Doctors.FirstOrDefault(d => d.Email == request.Email && d.Password == request.Password);
            if (doctor != null)
            {
                return CreateLoggedUserDTO(doctor.Id, doctor.Email, "doctor");
            }

            return null;
        }

        public LoggedUserDTO CreateLoggedUserDTO(int id, string email, string role)
        {
            return new LoggedUserDTO
            {
                Id = id,
                Email = email,
                Role = role,
                Token = GerarJwt(id, email, role)
            };
        }

        private string GerarJwt(int id, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
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
