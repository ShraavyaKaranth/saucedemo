using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using NUnit.Framework;

namespace sauceLabs_PageObject.Utils
{
    public class EmailSender
    {
        public static void SendEmailWithGmail(string reportPath)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string senderEmail = "shravyakaranth64715@gmail.com"; // ✅ Replace with your Gmail
                string senderPassword = "gadc mhyr pkpx jljf"; // ✅ Use the Google App Password
                string recipientEmail = "shravyabahha@gmail.com";

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "SpecFlow Test Report",
                    Body = "Attached is the Extent Report from the latest test execution.",
                    IsBodyHtml = false
                };

                mail.To.Add(recipientEmail);

                if (File.Exists(reportPath))
                {
                    mail.Attachments.Add(new Attachment(reportPath));
                    TestContext.Progress.WriteLine("✅ Attached Extent Report");
                }
                else
                {
                    TestContext.Progress.WriteLine("❌ Report file not found!");
                }

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                smtp.Send(mail);
                TestContext.Progress.WriteLine("✅ Email sent successfully via Gmail SMTP!");
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"❌ Failed to send email via Gmail SMTP: {ex.Message}");
            }
        }
    }
}
