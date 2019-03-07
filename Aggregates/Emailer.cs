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
            try {

                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                if (string.IsNullOrWhiteSpace(apiKey))
                    System.Diagnostics.Trace.TraceWarning("SENDGRID_APIKEY NOT FOUND");

                var client = new SendGridClient(apiKey);

                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress("noreply@mbacnationals.com", "NOREPLY! MBAC Nationals"));

                var recipients = new List<EmailAddress> {
                    new EmailAddress("gibble@gmail.com", "Chad Hurd")
                };

                msg.AddTos(recipients);
                msg.SetSubject("MBAC Data Change");

                msg.AddContent(MimeType.Text, "Some data has changed.");

                //TODO: Check result
                var result = client.SendEmailAsync(msg).GetAwaiter().GetResult();                
            }
            catch (Exception x)
            {
                System.Diagnostics.Trace.TraceError("Emailer.Send: " + x.ToString());
            }
        }
    }
}
