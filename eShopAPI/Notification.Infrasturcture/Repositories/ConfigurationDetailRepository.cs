using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entity;
using Notification.Domain.Interface;
using Notification.Infrasturcture.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.Repositories
{
    public class ConfigurationDetailRepository : IConfigurationDetails
    {
        private readonly NotificationContext _notificationContext;
        public ConfigurationDetailRepository(NotificationContext notificationContext) 
        {
            _notificationContext = notificationContext;
        }
        public Task<bool> DeleteConfigurationDetails(string configurationKey)
        {
            var configurationDetail = _notificationContext.ConfigurationDetails.Where(x => x.ConfigurationKey == configurationKey).FirstOrDefault();
            if (configurationDetail != null)
            {
                _notificationContext.ConfigurationDetails.Remove(configurationDetail);
            }

            return Task.FromResult(true);
        }

        public async Task<List<ConfigurationDetail>> GetAllConfigurationDetails()
        {
            return await _notificationContext.ConfigurationDetails.ToListAsync();
        }

        public Task<ConfigurationDetail> GetConfigurationDetails(string configurationKey)
        {
            var configurationDetail = _notificationContext.ConfigurationDetails.Where(x => x.ConfigurationKey == configurationKey).FirstOrDefault();
            if (configurationDetail != null)
            {
                return Task.FromResult(configurationDetail);
            }
            return null;
        }

        public async Task<bool> SaveConfigurationDetails(ConfigurationDetail configurationDetail)
        {
            _notificationContext.ConfigurationDetails.Add(configurationDetail);
            await _notificationContext.SaveChangesAsync();
            return true;
        }

        public async Task<ConfigurationDetail> UpdateConfigurationDetails(ConfigurationDetail configurationDetail)
        {
            _notificationContext.ConfigurationDetails.Update(configurationDetail);
            await _notificationContext.SaveChangesAsync();
            return configurationDetail;
        }
    }
}
