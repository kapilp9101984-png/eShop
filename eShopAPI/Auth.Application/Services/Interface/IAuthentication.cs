using Auth.Application.VM;
using Auth.Domain.Entity;
namespace Auth.Application.Services.Interface
{
    public interface IAuthentication
    {
        public Task<User> VerifyUser(string username);
        public Task<AuthenticationReponseVM> GenerateJWTToken(User request, List<Role> roles);
    }
}
