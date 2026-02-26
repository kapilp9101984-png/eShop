using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.VM
{
    public class UserLoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Role { get; set; }
        public UserLoginVM()
        {
            Role = new List<string>();
        }
        public UserLoginVM(string userName, string password, List<string> role)
        {
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
