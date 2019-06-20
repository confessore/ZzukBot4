using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ZzukBot.Web.Services.Options;

namespace ZzukBot.Web.Services
{
    internal class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        EmailSenderOptions Options { get; }

        public Task SendEmailAsync(string recipient, string subject, string body)
        {
            return Execute(recipient, subject, body);
        }

        async Task Execute(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Host = Options.SmtpHost,
                Port = Convert.ToInt32(Options.SmtpPort),
                EnableSsl = true,
                Credentials = new NetworkCredential(Options.SmtpUser, Options.SmtpPassword)
            };
            using (var msg = new MailMessage(Options.SmtpEmail, recipient)
            {
                From = new MailAddress(Options.SmtpEmail, Options.SmtpName),
                Subject = subject,
                Body = body
            })
            {
                msg.IsBodyHtml = true;

                await smtpClient.SendMailAsync(msg);
            }
        }
    }
}
