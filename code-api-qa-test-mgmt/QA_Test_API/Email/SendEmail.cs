using System.Net.Mail;
using System.Net;

namespace QA_Test_Log.Email
{
    public class SendEmail
    {
        public static async Task SendEmailAsync(IConfiguration configuration,string receiverEmail, string emailBody, string subject)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = Convert.ToInt32(emailSettings["SmtpPort"]);
            var smtpUsername = emailSettings["SmtpUsername"];
            var smtpPassword = emailSettings["SmtpPassword"];
            var smtpReceiver = receiverEmail;
            var smtpFrom = emailSettings["SmtpFrom"];
            var smtpEnableSsl = Convert.ToBoolean(emailSettings["SmtpEnableSsl"]);

            var smtpClient = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = smtpEnableSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpFrom),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(smtpReceiver);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
