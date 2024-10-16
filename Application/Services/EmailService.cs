using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DDDNetCore.Domain;
using Microsoft.Extensions.Configuration;

namespace DDDNetCore.Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public async Task SendEmailAsync(Email emailObj)
        {
            var smtpConfig = _configuration.GetSection("Smtp");

            using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
            {
                client.Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]);
                client.EnableSsl = bool.Parse(smtpConfig["EnableSsl"]);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpConfig["SenderEmail"], smtpConfig["SenderName"]),
                    Subject = emailObj.Subject,
                    Body = emailObj.EmailContent,
                    IsBodyHtml = true
                };
            
                mailMessage.To.Add(new MailAddress(emailObj.Destination));

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}