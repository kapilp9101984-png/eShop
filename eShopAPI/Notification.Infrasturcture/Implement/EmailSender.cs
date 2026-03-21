using Notification.Domain.Entity;
using Notification.Domain.Interface;
using Notification.Infrasturcture.DTOModel;
using Notification.Infrasturcture.Interface;
using Notification.Infrasturcture.Utility;
using System.Net;
using System.Net.Mail;

namespace Notification.Infrasturcture.Implement
{
    public class EmailSender(IConfigurationDetails configurationDetails, IEmailFormatter emailFormatter) : IEmailSender
    {
        NetworkCredential _networkCredential;
        public string UserName { get; set; } = string.Empty;
        public string EmailID { get; set; } = string.Empty;

        public async Task SendEmailAsync(string to, string Name, List<string> cc, string subject, string body)
        {

            SmtpClient mySmtpClient = new SmtpClient();
            mySmtpClient.Host = "my.smtp.exampleserver.net";
            mySmtpClient.Port = 587;
            mySmtpClient.EnableSsl = true;


            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            //System.Net.NetworkCredential basicAuthenticationInfo = new
            //   System.Net.NetworkCredential("username", "password");
            mySmtpClient.Credentials = _networkCredential;

            // add from,to mailaddresses
            MailAddress from = new MailAddress(EmailID, UserName);
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

        public async Task SendUserAccountLockedEmailAsync(UserCreateDTO userCreateDTO)
        {
            var AutheEmailDetail = await configurationDetails.GetConfigurationDetails("AuthEmailID");
            var AutheEmailPassword = await configurationDetails.GetConfigurationDetails("AuthEmailPassword");

            _networkCredential = new
               System.Net.NetworkCredential(AutheEmailDetail.IsSecret ? AutheEmailDetail.Value.Decrypt("") : AutheEmailDetail.Value, AutheEmailPassword.IsSecret ? AutheEmailPassword.Value.Decrypt("") : AutheEmailPassword.Value);

            var newUserEmailFormatter = await emailFormatter.GetEmailFormat("UserCreated");
            var modifiedBody = newUserEmailFormatter.Body.Replace("{FirstName}", userCreateDTO.FirstName).Replace("{LastName}", userCreateDTO.LastName);
            SendEmailAsync(userCreateDTO.Email, userCreateDTO.FirstName + " " + userCreateDTO.LastName, null, newUserEmailFormatter.Subject, newUserEmailFormatter.Body);

            //throw new NotImplementedException();
        }

        public Task SendUserCreateEmailAsync(UserCreateDTO userCreateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
