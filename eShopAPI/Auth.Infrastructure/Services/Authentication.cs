using Auth.Application.Services.Interface;
using Auth.Application.VM;
using Auth.Domain.Entity;
using Auth.Domain.Interface;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Model;

namespace Auth.Infrastructure.Services
{
    public class Authentication : IAuthentication
    {
        IUser user;
        public Authentication(IUser _user)
        {
            user = _user;
        }
        public Task<AuthenticationReponseVM> GenerateJWTToken(User request,List<Role> roles)
        {
            JWTTokenHandler jWTTokenHandler = new JWTTokenHandler();
            var generateJWTAuth =  jWTTokenHandler.GenerateJwtToken(new AuthenticationRequest {UserName = request.UserName , Role =  roles.Select(p=>p.Name).ToList() });
            return generateJWTAuth != null ? Task.FromResult(new AuthenticationReponseVM { UserName = generateJWTAuth.UserName, Token = generateJWTAuth.Token, ExpireIn = generateJWTAuth.ExpireIn }) : Task.FromResult<AuthenticationReponseVM>(null);
        }

        public async Task<User> VerifyUser(string username)
        {
            User userData = await user.GetUser(username);
            return userData;
        }
    }
}
