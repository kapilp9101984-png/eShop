using Notification.Domain.Entity;
using Notification.Domain.Interface;
using Notification.Infrasturcture.Context;

namespace Notification.Infrasturcture.Repositories
{
    public class ConfigurationDetailAzureRepository : IConfigurationDetails
    {
        
        public ConfigurationDetailAzureRepository(NotificationContext notificationContext) 
        {
            
        }
        public Task<bool> DeleteConfigurationDetails(string configurationKey)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ConfigurationDetail>> GetAllConfigurationDetails()
        {
            throw new NotImplementedException();
        }

        public Task<ConfigurationDetail> GetConfigurationDetails(string configurationKey)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveConfigurationDetails(ConfigurationDetail configurationDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ConfigurationDetail> UpdateConfigurationDetails(ConfigurationDetail configurationDetail)
        {
            throw new NotImplementedException();
        }
    }
}
