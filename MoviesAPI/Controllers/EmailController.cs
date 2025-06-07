using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.EmailReset;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("Email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly UserService _userService;

        public EmailController(EmailService emailService, UserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult EnviarEmail([FromBody] string address)
        {
            try
            {
                string subject = "Password Recovery";
                var token = GeneratePasswordResetToken(address);

                if (token == null)
                    throw new Exception("There was an error generating the token");

                var link = GenerateResetLink(token);
                var body = $"Click here to reset your password: <a href=\"{link}\">Reset password</a>";
                _emailService.EnviarEmail(address, subject, body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"There was an error sending the email: {ex.Message}");
            }
        }

        private string? GeneratePasswordResetToken(object address)
        {
            var user = _userService.FindByEmail(address);
            if (user == null)
                return null;

            var token = Guid.NewGuid().ToString();

            _emailService.SavePasswordResetToken(user.Id, token, DateTime.UtcNow.AddHours(1));

            return token;
        }

        private string GenerateResetLink(string token)
        {
            var baseUrl = "localhost:3000/reset-password"; 
            return $"{baseUrl}?token={token}";
        }
    }
}
