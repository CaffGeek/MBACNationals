using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Emailer
{
    public static string apiKey
    {
        get { return Environment.GetEnvironmentVariable("SENDGRID_APIKEY"); }
    }

    public static void SendMatchCompleteNotification(List<string> recipients, string title, string message)
    {
        try
        {            
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                System.Diagnostics.Trace.TraceWarning("SENDGRID_APIKEY NOT FOUND");
                return;
            }

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("MbacNationalScores@Gmail.com", "NOREPLY! MBAC Nationals"));

            var recipientEmails = recipients.Select(x => new EmailAddress(x)).ToList();
            msg.AddTos(recipientEmails);

            msg.SetSubject($"MBAC Match Complete - {title}");

            msg.AddContent(MimeType.Html, message);

            //TODO: Check result
            var result = client.SendEmailAsync(msg).GetAwaiter().GetResult();
        }
        catch (Exception x)
        {
            System.Diagnostics.Trace.TraceError("Emailer.Send Exception: " + x.ToString());
        }
    }

    public static void SendChangeNotification(DateTime cutoff, List<String> recipients, string title, string message)
    {
        try {
            if (DateTime.Now < cutoff)
                return;
            
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                System.Diagnostics.Trace.TraceWarning("SENDGRID_APIKEY NOT FOUND");
                return;
            }

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("noreply@mbacnationals.com", "NOREPLY! MBAC Nationals"));
            
            var recipientEmails = recipients.Select(x => new EmailAddress(x)).ToList();                
            msg.AddTos(recipientEmails);

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