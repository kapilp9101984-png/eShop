using Notification.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Domain.Interface
{
    public interface IConfigurationDetails
    {
        public Task<ConfigurationDetail> GetConfigurationDetails(string configurationName);
        public Task<List<ConfigurationDetail>> GetAllConfigurationDetails();

        public Task<bool> SaveConfigurationDetails(ConfigurationDetail configurationDetail);

        public Task<ConfigurationDetail> UpdateConfigurationDetails(ConfigurationDetail configurationDetail);

        public Task<bool> DeleteConfigurationDetails(string configurationName);
    }
}
