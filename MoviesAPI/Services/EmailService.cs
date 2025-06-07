using MoviesAPI.Data;
using MoviesAPI.Models;
using System.Net;
using System.Net.Mail;

namespace MoviesAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly APIContext _context;


        public EmailService(IConfiguration config, APIContext context)
        {
            _config = config;
            _context = context;
        }

        public void EnviarEmail(string address, string subject, string body)
        {
            var sender = _config["Email:Usuario"];
            var pass = _config["Email:Senha"];

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, pass),
                EnableSsl = true,
            };

            var message = new MailMessage(sender, address, subject, body);
            smtp.Send(message);
        }

        public void SavePasswordResetToken(int id, string token, DateTime dateTime)
        {
            var resetToken = new EmailResetToken
            {
                UserId = id,
                Token = token,
                expire = dateTime
            };

            _context.ResetTokens.Add(resetToken);
            _context.SaveChanges();
        }

        public EmailResetToken? GetPasswordResetToken(string token)
        {
            return _context.ResetTokens.FirstOrDefault(r => r.Token.Equals(token));
        }

        public bool DeleteToken(string token)
        {
            var resetToken = _context.ResetTokens.FirstOrDefault(r => r.Token.Equals(token));
            if (resetToken != null) {
                _context.ResetTokens.Remove(resetToken);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
