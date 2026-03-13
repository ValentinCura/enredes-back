using Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailFrom;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;

        public MailService(IConfiguration configuration)
        {
            _mailFrom = configuration["mailSettings:mailFromAddress"] ?? "";
            _smtpServer = configuration["mailSettings:smtpServer"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(configuration["mailSettings:smtpPort"] ?? "587");
            _smtpUser = configuration["mailSettings:smtpUser"] ?? "";
            _smtpPassword = configuration["mailSettings:smtpPassword"] ?? "";
        }

        public void Send(string subject, string message, string mailTo)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Enredes", _mailFrom));
            email.To.Add(new MailboxAddress("", mailTo));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = message
            };

            using var client = new SmtpClient();
            client.Connect(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(_smtpUser, _smtpPassword);
            client.Send(email);
            client.Disconnect(true);
        }
    }
}