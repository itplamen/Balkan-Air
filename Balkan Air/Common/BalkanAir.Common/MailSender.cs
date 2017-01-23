namespace BalkanAir.Common
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public sealed class MailSender
    {
        private const string SEND_FROM = "itplamen@gmail.com";
        private const string SEND_FROM_NAME = "Balkan Air Bulgaria";
        //private const string PASSWORD = "__YOUR_PASSWORD_HERE__";
        private const string PASSWORD = "Common1ypeSys1em";
        
        private const string HOST = "smtp.gmail.com";
        private const int PORT = 587;

        private static readonly object SyncRoot = new object();
        
        private static MailSender instance;
        private readonly SmtpClient mailClient;
        private readonly NetworkCredential networkCredential;

        private MailSender()
        {
            this.networkCredential = new NetworkCredential(SEND_FROM, PASSWORD);

            this.mailClient = new SmtpClient
            {
                Host = HOST,
                Port = PORT,
                EnableSsl = true,
                UseDefaultCredentials = true,
                Credentials = this.networkCredential
            };
        }

        public static MailSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MailSender();
                        }
                    }
                }

                return instance;
            }
        }

        public void SendMailAsync(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            this.mailClient.SendAsync(message, null);
        }

        public void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            this.mailClient.Send(message);
        }

        private MailMessage PrepareMessage(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients)
        {
            var mailTo = new MailAddress(recipient);
            var mailFrom = new MailAddress(SEND_FROM, SEND_FROM_NAME);
            var message = new MailMessage(mailFrom, mailTo)
            {
                Body = messageBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
            };

            if (bccRecipients != null)
            {
                foreach (var bccRecipient in bccRecipients)
                {
                    message.Bcc.Add(bccRecipient);
                }
            }

            return message;
        }
    }
}
