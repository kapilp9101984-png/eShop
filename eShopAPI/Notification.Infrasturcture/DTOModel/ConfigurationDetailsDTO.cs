using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.DTOModel
{
    public class ConfigurationDetailsDTO
    {
        public required string ConfigurationKey { get; set; }
        public string Value { get; set; } = string.Empty;
        public bool IsSecret { get; set; } = false;
    }
}
