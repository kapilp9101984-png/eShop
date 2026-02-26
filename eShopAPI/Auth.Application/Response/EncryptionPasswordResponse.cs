
namespace Auth.Application.Response
{
    public class EncryptionPasswordResponse
    {
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
