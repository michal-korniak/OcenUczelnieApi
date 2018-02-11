using OcenUczelnie.Infrastructure.Services.Interfaces;
using OcenUczelnie.Infrastructure.Settings;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System;

namespace OcenUczelnie.Infrastructure.Services {
    public class EmailSender : IEmailSender {
        private readonly EmailSettings _emailSettings;

        public EmailSender (EmailSettings emailSettings) {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync (string receiver, string subject, string body) {
            var client = GetSmtpClient ();
            var mailMessage = new MailMessage {
                From = new MailAddress (_emailSettings.Email),
                To = { receiver },
                Subject = subject,
                Body = body
            };
            try{
                await client.SendMailAsync (mailMessage);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally{
                client.Dispose ();
            }
        }

        private SmtpClient GetSmtpClient () {
            return new SmtpClient {
                Host = _emailSettings.Host,
                    EnableSsl = _emailSettings.EnableSsl,
                    Port = _emailSettings.Port,
                    Timeout = _emailSettings.Timeout,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential (_emailSettings.Email, _emailSettings.Password)
            };
        }

    }
}