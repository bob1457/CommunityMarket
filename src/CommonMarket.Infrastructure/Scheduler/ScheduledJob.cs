using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Infrastructure.Email;
using Quartz;
using Quartz.Impl;

namespace CommonMarket.Infrastructure.Scheduler
{
    public class ScheduledJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();

            //using (var message = new MailMessage("user@gmail.com", "user@live.co.uk"))
            //{
            //    message.Subject = "Test";
            //    message.Body = "Test at " + DateTime.Now;
            //    using (SmtpClient client = new SmtpClient
            //    {
            //        EnableSsl = true,
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        Credentials = new NetworkCredential("user@gmail.com", "password")
            //    })
            //    {
            //        client.Send(message);
            //    }
            //}

            var emailNotification = new EmailNotification();
            
            emailNotification.SendEmail("bob.yuan@yahoo.com", "Test of Scheduled Task","Test message sent at " + DateTime.Now );
        }
    }
}
