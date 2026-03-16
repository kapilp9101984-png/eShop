using Notification.Domain.Entity;

namespace Notification.Domain.Interface
{
    public interface IEmailFormatter
    {
        public Task<EmailFormatter> GetEmailFormat(string templateName);
        public Task<bool> SaveEmailFormat(EmailFormatter emailFormatter);
        public Task<bool> UpdateEmailFormat(EmailFormatter emailFormatter);
        public Task<List<EmailFormatter>>  GetEmailFormats();

    }
}
