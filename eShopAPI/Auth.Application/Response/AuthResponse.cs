using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Response
{
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public ResponseStatus Status { get; set; }
    }

    public enum ResponseStatus
    { 
        Success,
        NotActivated,
        UnAuthorized,
        NotEmailVerified,
        RoleNotGranted,
    }
}
