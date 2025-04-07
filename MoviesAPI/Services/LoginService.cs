using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Login;
using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesAPI.Services
{
    public class LoginService
    {
        private readonly APIContext _context;
        private readonly IConfiguration _config;

        public LoginService(APIContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public UserDTO? Login(LoginRequest request)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return null;
            }

            return CreateUserDTO(user);
        }

        public UserDTO CreateUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Role = "user",
                Token = GerarJwt(user)
            };
        }

        private string GerarJwt(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, "user"),
            new Claim("Id", user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
