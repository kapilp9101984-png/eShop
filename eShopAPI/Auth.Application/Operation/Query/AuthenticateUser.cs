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
                                ExpireOn = DateTime.Now.AddMinutes(30),
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                IPAddress = string.Empty,
                                IsLogOut = false
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
                        response.Message = "Role not granted for this user , please contact to admin.";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.UnAuthorized;
                    if ((3 - loggedInUser.FailedLoginAttempts) > 0)
                    {
                        response.Message = "User name and password not matched,only " + (3 - loggedInUser.FailedLoginAttempts) + " attepts are left.";
                    }
                    else
                    {
                        response.Message = "User name and password not matched,only you reached maximum attempts now your account locked.";
                    }
                }
            }
            else if (loggedInUser != null && !loggedInUser.IsActive)
            {
                response.Status = ResponseStatus.NotActivated;
                response.Message = "User name and password not matched,only you reached maximum attempts now your account locked.";
            }
            else if (loggedInUser != null && !loggedInUser.IsEmailVerified)
            {
                response.Status = ResponseStatus.NotActivated;
                response.Message = "Your account email id verfication is still pending, please verify you account first.";
            }
            else
            {
                response.Status = ResponseStatus.UnAuthorized;
                response.Message = "User name and password not verified please contact to administrator!";
            }
            // TODO: implement authentication logic and return JWT/token
            return response;
        }
    }
}
