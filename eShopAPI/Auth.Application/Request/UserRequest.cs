using Auth.Application.DTO;
using MediatR;

namespace Auth.Application.Request
{
   
    public class UserRequest : IRequest<string>
    {
        public UserDTO UserDTO { get; set; }

        public UserRequest(UserDTO _userDTO)
        {
            UserDTO = _userDTO;
        }
    }
}
