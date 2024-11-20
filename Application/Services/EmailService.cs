using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DDDNetCore.Domain;
using Microsoft.Extensions.Configuration;

namespace DDDNetCore.Application.Services
{
    /**
     * Service responsible for sending emails using the SMTP protocol.
     */
    public class EmailService
    {
        /**
         * Configuration instance used to retrieve SMTP settings from the configuration file.
         */
        private readonly IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true).Build();

        /**
         * Sends an email asynchronously to the specified recipient with the provided content.
         *
         * @param emailObj An object containing the email content, subject, and destination information.
         * @return A task representing the asynchronous operation.
         * @throws Throws an exception if the email cannot be sent due to network or SMTP configuration issues.
         */
        public async Task SendEmailAsync(Email emailObj)
        {
            var smtpConfig = _configuration.GetSection("Smtp");

            using var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]));
            client.Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpConfig["SenderEmail"], smtpConfig["SenderName"]),
                Subject = emailObj.Subject,
                Body = emailObj.EmailContent,
                IsBodyHtml = true
            };
            
            mailMessage.To.Add(new MailAddress(emailObj.Destination));

            try
            {
                await client.SendMailAsync(mailMessage);
                Console.WriteLine($"Successfully sent the email to {emailObj.Destination}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine($"Failed to send the email to {emailObj.Destination}.");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}