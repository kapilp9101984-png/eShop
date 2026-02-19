using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuthenticationManager.Model
{
    public class UserAccounts
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
