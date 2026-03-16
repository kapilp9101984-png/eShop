using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entity;
using Notification.Domain.Interface;
using Notification.Infrasturcture.Context;

namespace Notification.Infrasturcture.Repositories
{
    public class EmailFormatterRepository : IEmailFormatter
    {
        private readonly NotificationContext _db;

        public EmailFormatterRepository(NotificationContext db)
        {
            _db = db;
        }

        public async Task<EmailFormatter> GetEmailFormat(string templateName)
        {
            return await _db.EmailFormatters.Where(x => x.TemplateName == templateName).FirstOrDefaultAsync();            
        }

        public async Task<List<EmailFormatter>> GetEmailFormats()
        {
            return await _db.EmailFormatters.ToListAsync();
        }
        

        async Task<bool> IEmailFormatter.SaveEmailFormat(Domain.Entity.EmailFormatter emailFormatter)
        {
            _db.EmailFormatters.Add(emailFormatter);
            await _db.SaveChangesAsync();
            return true;
        }

        async Task<bool> IEmailFormatter.UpdateEmailFormat(Domain.Entity.EmailFormatter emailFormatter)
        {
            _db.EmailFormatters.Update(emailFormatter);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
