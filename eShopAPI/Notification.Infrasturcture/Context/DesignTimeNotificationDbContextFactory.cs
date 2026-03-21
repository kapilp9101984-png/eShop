using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.Context
{
   
    public class DesignTimeNotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationContext>
    {
        public NotificationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationContext>();
            optionsBuilder.UseSqlite("Data Source=C:/sqlite/eShopNotication.db");
            return new NotificationContext(optionsBuilder.Options);
        }
    }
}
