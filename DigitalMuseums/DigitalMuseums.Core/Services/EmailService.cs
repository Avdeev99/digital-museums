using System.Net;
using System.Net.Mail;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace DigitalMuseums.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpClientOptions _smtpClientOptions;

        public EmailService(IOptions<SmtpClientOptions> smtpClientOptions)
        {
            _smtpClientOptions = smtpClientOptions.Value;
            _smtpClient = new SmtpClient(_smtpClientOptions.Smtp, _smtpClientOptions.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpClientOptions.SenderEmail, _smtpClientOptions.SenderPassword)
            };
        }

        public void Send(string mailTo, string mailSubject, string body)
        {
            var from = new MailAddress(_smtpClientOptions.SenderEmail, _smtpClientOptions.EmailTitle);
            var to = new MailAddress(mailTo);
            var mail = new MailMessage(from, to)
            {
                Subject = mailSubject,
                Body = body
            };

            _smtpClient.Send(mail);
        }
    }
}
