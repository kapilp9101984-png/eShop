using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entity;

namespace Notification.Infrasturcture.Context
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
            
        }

        public DbSet<EmailFormatter> EmailFormatters { get; set; }
        public DbSet<MobileFormatter> MobileFormatters { get; set; }
        public DbSet<ConfigurationDetail> ConfigurationDetails { get; set; }
    }
}
