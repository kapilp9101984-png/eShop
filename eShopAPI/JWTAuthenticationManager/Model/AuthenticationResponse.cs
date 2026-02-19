using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuthenticationManager.Model
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpireIn {  get; set; }
    }
}
