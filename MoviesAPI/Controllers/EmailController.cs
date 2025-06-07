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
                    throw new Exception("Erro ao gerar o Token");

                var link = GenerateResetLink(token);
                var body = $"Clique aqui para redefinir sua senha: <a href=\"{link}\">Redefinir Senha</a>";
                _emailService.EnviarEmail(address, subject, body);
                return Ok("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
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

        public string GenerateResetLink(string token)
        {
            var baseUrl = "localhost:3000/redefinir-senha"; 
            return $"{baseUrl}?token={token}";
        }
    }
}
