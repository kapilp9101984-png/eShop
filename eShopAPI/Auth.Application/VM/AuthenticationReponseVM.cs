using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.VM
{
    public class AuthenticationReponseVM
    {

        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpireIn { get; set; }
        public string RefreshToken { get; set; }

    }
}
