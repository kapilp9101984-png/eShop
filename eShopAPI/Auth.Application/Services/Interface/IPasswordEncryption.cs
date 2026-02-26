
using Auth.Application.Response;

namespace Auth.Application.Services.Interface
{
    public interface IEncryption
    {
        public EncryptionPasswordResponse PasswordEncryption(string password);
        public bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}
