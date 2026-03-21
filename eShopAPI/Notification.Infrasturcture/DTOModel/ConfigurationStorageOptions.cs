using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.DTOModel
{
    public class ConfigurationStorageOptions
    {
        public bool UseAzure { get; set; }
        public string EncryptionKey { get; set; } = string.Empty;
    }
}
