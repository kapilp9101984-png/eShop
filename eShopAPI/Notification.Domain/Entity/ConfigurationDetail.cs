using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Domain.Entity
{
    public class ConfigurationDetail
    {
        public int ID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public string EncryptedKey { get; set; } = string.Empty;
        public required string ConfigurationName { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
