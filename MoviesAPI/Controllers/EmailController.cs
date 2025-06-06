using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("Email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult EnviarEmail([FromBody] string address)
        {
            try
            {
                _emailService.EnviarEmail(address);
                return Ok("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }
}
