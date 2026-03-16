using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.DTOModel
{
    public class UserCreateDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }

    }
}
