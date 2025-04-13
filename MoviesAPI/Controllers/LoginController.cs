using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.Login;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{

    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {

        private readonly LoginService _loginService;
        private readonly JWTService _JWTService;


        public LoginController(LoginService loginService, JWTService jWTService)
        {
            _loginService = loginService;
            _JWTService = jWTService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            UserDTO? user = _loginService.Login(request);
            if (user == null)
            {
                return Unauthorized(new { message = "Credenciais inválidas" });
            }
            return Ok(user);
        }
    }
}
