using Auth.Application.DTO;
using Auth.Application.Request;
using Auth.Application.Response;
using Auth.Application.Services.Interface;
using Auth.Domain.Entity;
using Auth.Domain.Interface;
using MediatR;

namespace Auth.Application.Operation.Command
{
    public class CreateUser(IUser _userRepo, IMapper mapper, IEncryption encryption) : IRequestHandler<UserRequest, string>
    {
        public IUser UserRepositry { get; set; } = _userRepo;
        public IMapper Mapper { get; set; } = mapper;
        public IEncryption Encryption { get; set; } = encryption;

        public async Task<string> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            User user = Mapper.Map<UserDTO, User>(request.UserDTO);
            if (user == null) { return "User Not Created !"; }
            if (string.IsNullOrEmpty(request.UserDTO.Password))
            {
                return "Password is required !";
            }

            EncryptionPasswordResponse passwordEncryption= Encryption.PasswordEncryption(request.UserDTO.Password);

            user.PasswordHash = passwordEncryption.PasswordHash;
            user.PasswordSalt = passwordEncryption.PasswordSalt;
            user.IsLocked = false;
            if (await UserRepositry.CreateUser(user))
            {
                return "User created successfull, you will get email activation email soon.";
            }
            else 
            { 
                return "User Not Created ! Someting went wrong.";
            }
        }
    }
}
