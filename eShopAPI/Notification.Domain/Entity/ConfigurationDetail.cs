using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Domain.Entity
{
    public class ConfigurationDetail
    {
        public int ID { get; set; }      
        public required string ConfigurationKey { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;   
        public bool IsSecret { get; set; } = false;
        public bool IsActive { get; set; } = true;

    }
}
