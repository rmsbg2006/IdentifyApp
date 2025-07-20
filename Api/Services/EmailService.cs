using Api.Data;
using Api.DTOs.Account;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailSendDto emailSend);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(EmailSendDto emailSend)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email, emailSend.To, emailSend.Subject, emailSend.Body);
            await smtpClient.SendMailAsync(message);

            return true;
        }
    }
}
