using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuthenticationManager.Model
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
