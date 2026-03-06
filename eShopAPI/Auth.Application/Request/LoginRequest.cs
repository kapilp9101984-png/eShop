using Auth.Application.Response;
using MediatR;

namespace Auth.Application.Request
{
    public class LoginRequest : IRequest<AuthResponse>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
