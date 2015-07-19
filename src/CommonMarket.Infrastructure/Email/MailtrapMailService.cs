using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace CommonMarket.Infrastructure.Email
{
    public class MailtrapMailService : IIdentityMessageService //Fake SMTP for testing purposes
    {
        public Task SendAsync(IdentityMessage message)
        {
            var client = new SmtpClient
            {
                Host = "mailtrap.io",
                Port = 2525,
                Credentials = new NetworkCredential("bob.yuan@yahoo.com", "unity100email"),
                EnableSsl = true,
            };

            var @from = new MailAddress("no-reply@tech.trailmax.info", "My Awesome Admin");
            var to = new MailAddress(message.Destination);

            var mail = new MailMessage(@from, to)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
            };

            client.Send(mail);

            return Task.FromResult(0);
        }
    }
}
