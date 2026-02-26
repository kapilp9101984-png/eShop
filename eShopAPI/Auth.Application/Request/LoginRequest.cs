using Auth.Application.VM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Request
{
    public class LoginRequest : IRequest<UserLoginVM>
    {
        public UserLoginVM UserLogin { get; set; }
        public LoginRequest(UserLoginVM userLoginVM) {
            userLoginVM = userLoginVM;
        }
    }
}
