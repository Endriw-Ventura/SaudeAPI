using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.EmailReset;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    
    [ApiController]
    [Route("Reset")]
    public class ResetController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly UserService _userService;


        public ResetController(EmailService emailService, UserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var tokenInfo = _emailService.GetPasswordResetToken(dto.Token);

            if (tokenInfo == null || tokenInfo.expire < DateTime.UtcNow)
                return BadRequest("Invalid or expired token");

            var user = _userService.GetUserByID(tokenInfo.Id);
            if (user == null)
                return NotFound();

            _userService.EditPasswordReset(user, dto.NewPassword);

            var success = _emailService.DeleteToken(dto.Token);
            if (!success)
                return BadRequest("Invalid or expired token");

            return Ok("Password reset successfully!");
        }
    }
}
