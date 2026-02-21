using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Common
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive {  get; set; }
        public bool IsDeleted { get; set; }
    }
}
