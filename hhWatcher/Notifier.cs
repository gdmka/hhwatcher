using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace hhWatcher
{
    public static class Notifier
    {
        public static void Notify(string pos, List<string> newHires)
        {

            string subject = $"Count is {pos} now";
            string body = "";

            foreach (var h in newHires)
            {
                body += $"<p>{h}<p>";
            }

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(Conf.postMasterEmail);
                mail.To.Add(Conf.recipientEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(Conf.postMasterSMTPAddr, Conf.postMasterPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(Conf.postMasterEmail, Conf.postMasterPW);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
