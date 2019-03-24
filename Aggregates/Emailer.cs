using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;

public static class Emailer
{
    public static void Send(string title, string message)
    {
        try {
            var cutoff = DateTime.Parse((ConfigurationManager.AppSettings["notificationCutoff"] ?? "June 1") + ", " + DateTime.Now.Year);
            if (DateTime.Now < cutoff)
            {
                return;
            }

            var email = ConfigurationManager.AppSettings["notificationEmail"];
            if (string.IsNullOrWhiteSpace(email))
            {
                System.Diagnostics.Trace.TraceWarning("Notification Email Missing/Malformed");
                return;
            }

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                System.Diagnostics.Trace.TraceWarning("SENDGRID_APIKEY NOT FOUND");
                return;
            }

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("noreply@mbacnationals.com", "NOREPLY! MBAC Nationals"));

            var recipients = new List<EmailAddress> {
                new EmailAddress(email),
            };

            msg.AddTos(recipients);
            msg.SetSubject($"MBAC Data Change - {title}");

            msg.AddContent(MimeType.Text, message);

            //TODO: Check result
            var result = client.SendEmailAsync(msg).GetAwaiter().GetResult();
        }
        catch (Exception x)
        {
            System.Diagnostics.Trace.TraceError("Emailer.Send Exception: " + x.ToString());
        }
    }
}