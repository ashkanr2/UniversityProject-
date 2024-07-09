using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using UniversityProject.Interfaces;
using UniversityProject.Models.EmailSetting;

namespace UniversityProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var mail = "University@ashkanr2.ir";
                var pw = "Ad!@#Unier";


                var client = new SmtpClient("mail.ashkanr2.ir")
                {
                    EnableSsl = false,
                    Credentials = new NetworkCredential(mail, pw),
                    UseDefaultCredentials = false,
                    


                };
               
                await client.SendMailAsync(
                    new MailMessage(
                        from: mail,
                        to: toEmail,
                        subject: subject,
                        message
                        ));

                return true;




                //var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
                //{
                //    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                //    EnableSsl = true
                //};

                //// Ignore SSL certificate validation
                //ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                //var mailMessage = new MailMessage
                //{
                //    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                //    Subject = subject,
                //    Body = message,
                //    IsBodyHtml = true
                //};
                //mailMessage.To.Add(toEmail);

                //await client.SendMailAsync(mailMessage);
                //return true; // Return true if email sent successfully
            }
            catch (Exception ex)
            {
                // Handle exceptions here (log them, throw custom exceptions, etc.)
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false; // Return false indicating failure
            }
        }
    }
}
