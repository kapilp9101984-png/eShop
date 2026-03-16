using Notification.Infrasturcture.DTOModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrasturcture.Interface
{
    public interface IEmailSender
    {        
        Task SendUserCreateEmailAsync(UserCreateDTO userCreateDTO);

        Task SendUserAccountLockedEmailAsync(UserCreateDTO userCreateDTO);
    }
}
