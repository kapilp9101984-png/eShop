using Auth.Application.Request;
using Auth.Application.Response;
using Auth.Application.Services.Interface;
using Auth.Domain.Entity;
using Auth.Domain.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Operation.Query
{
    public class AuthenticateUser : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly IAuthentication authentication;
        private readonly IEncryption encryption;
        private readonly IRole role;
        private readonly IUserInRole userInRole;
        private readonly IRefreshToken refreshToken;
        public AuthenticateUser(IAuthentication _authentication, IEncryption _encryption, IRole _role,
            IUserInRole _userInRole, IRefreshToken _refreshToken)
        {
            authentication = _authentication;
            encryption = _encryption;
            role = _role;
            userInRole = _userInRole;
            refreshToken = _refreshToken;
        }
        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            AuthResponse response = new AuthResponse();

            User loggedInUser = await authentication.VerifyUser(request.UserName);

            if (loggedInUser != null && loggedInUser.IsEmailVerified && loggedInUser.IsActive)
            {
                if (encryption.VerifyPassword(request.Password, loggedInUser.PasswordHash, loggedInUser.PasswordSalt))
                {

                    List<Role> userRoles = await userInRole.GetRoleOfUser(loggedInUser.ID);
                    if (userRoles != null && userRoles.Count > 0)
                    {
                        var JWTAuthToken = await authentication.GenerateJWTToken(loggedInUser, userRoles);

                        if (JWTAuthToken != null)
                        {
                            var token = Guid.NewGuid().ToString();
                            await refreshToken.CreateRefreshToken(new RefreshToken()
                            {
                                UserID = loggedInUser.ID,
                                Token = token,
                                IsLogOut = false,
                                ExpireOn = DateTime.Now.AddMinutes(30),
                                IsActive = true,
                                IsRevoke = false,
                                IsDeleted = false,
                                ReplacedByToken = string.Empty,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            });
                            response.RefreshToken = token;
                            response.Token = JWTAuthToken.Token;
                            response.ExpiresIn = JWTAuthToken.ExpireIn;
                            response.UserName = loggedInUser.UserName;
                            response.Status = ResponseStatus.Success;

                        }
                    }
                    else
                    {
                        response.Status = ResponseStatus.RoleNotGranted;
                    }
                }
                else
                {
                    response.Status = ResponseStatus.UnAuthorized;
                }
            }
            else if (loggedInUser != null && !loggedInUser.IsActive)
            {
                response.Status = ResponseStatus.NotActivated;
            }
            else if (loggedInUser != null && !loggedInUser.IsEmailVerified)
            {
                response.Status = ResponseStatus.NotActivated;
            }
            // TODO: implement authentication logic and return JWT/token
            return response;
        }
    }
}
