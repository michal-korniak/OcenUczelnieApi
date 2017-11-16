using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string receiver, string subject, string body)
        {
            var client = GetSmtpClient();
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email),
                To = {receiver},
                Subject = subject,
                Body = body
            };
            await client.SendMailAsync(mailMessage);
            client.Dispose();
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = _emailSettings.Host,
                EnableSsl = _emailSettings.EnableSsl,
                Port = _emailSettings.Port,
                Timeout = _emailSettings.Timeout,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
            };
        }
        
    }
}