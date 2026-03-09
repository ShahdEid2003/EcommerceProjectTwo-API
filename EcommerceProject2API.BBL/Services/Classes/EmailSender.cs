using EcommerceProject2API.BBL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("shahdeid012@gmail.com", "xdpg hcwi ewuf pepw")
            };

            return client.SendMailAsync(
                new MailMessage(from: "shahdeid012@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                { IsBodyHtml = true }
                );

        }
    }
}
