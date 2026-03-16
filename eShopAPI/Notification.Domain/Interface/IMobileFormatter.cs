using Notification.Domain.Entity;

namespace Notification.Domain.Interface
{
    public interface IMobileFormatter
    {
        public Task<EmailFormatter> GetMobileFormat(string templateName);
        public Task<bool> SaveMobileFormat(MobileFormatter emailFormatter);
        public Task<bool> UpdateMobileFormat(MobileFormatter emailFormatter);
        public Task<List<MobileFormatter>> GetAllMobileFormat();

    }
}
