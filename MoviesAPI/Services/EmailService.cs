using MoviesAPI.Models;
using System.Net;
using System.Net.Mail;

namespace MoviesAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void EnviarEmail(string address)
        {
            var remetente = _config["Email:Usuario"];
            var senha = _config["Email:Senha"];

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(remetente, senha),
                EnableSsl = true,
            };

            var message = new MailMessage(remetente, address, "Password Recovery", "Link to recover your password");
            smtp.Send(message);
        }
    }
}
