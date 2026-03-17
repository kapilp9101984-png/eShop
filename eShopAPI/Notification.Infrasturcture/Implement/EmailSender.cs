using Notification.Infrasturcture.DTOModel;
using Notification.Infrasturcture.Interface;
using System.Net.Mail;

namespace Notification.Infrasturcture.Implement
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string to,string Name, List<string> cc, string subject, string body)
        {
            
            SmtpClient mySmtpClient = new SmtpClient();
            mySmtpClient.Host = "my.smtp.exampleserver.net";
            mySmtpClient.Port = 587; 
            mySmtpClient.EnableSsl = true;

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential("username", "password");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress from = new MailAddress("donotreply@example.com", "TestFromName");
            MailAddress To = new MailAddress(to, string.IsNullOrEmpty(Name) ? to : Name);
            MailMessage myMail = new System.Net.Mail.MailMessage(from, To);

            // add CC TO

            foreach (var ccEmail in cc)
            {
                MailAddress ccAddress = new MailAddress(ccEmail);
                myMail.CC.Add(ccAddress);
            }


            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = body;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);

            Task.CompletedTask.Wait();


        }

        public Task SendUserAccountLockedEmailAsync(UserCreateDTO userCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task SendUserCreateEmailAsync(UserCreateDTO userCreateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
