using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;

namespace Emailer
{
    public static class Emailer
    {
        public static void Send()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            if (string.IsNullOrWhiteSpace(apiKey))
                return;

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("noreply@mbacnationals.com", "NOREPLY! MBAC Nationals"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress("gibble@gmail.com", "Chad Hurd")
            };

            msg.AddTos(recipients);
            msg.SetSubject("MBAC Data Change");

            msg.AddContent(MimeType.Text, "Some data has changed.");

            var result = client.SendEmailAsync(msg).GetAwaiter().GetResult();
        }
    }
}
