using Auth.Application.Response;
using Auth.Application.Services.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Auth.Infrastructure.Services
{
    public class Encryption : IEncryption
    {
        private const int IterationCount = 600000;
        // NIST recommends a salt length of at least 128 bits (16 bytes).
        private const int SaltSize = 16;
        // Derive a 256-bit (32-byte) subkey.
        private const int DerivedKeySize = 32;

        public EncryptionPasswordResponse PasswordEncryption(string password)
        {
           EncryptionPasswordResponse response = new EncryptionPasswordResponse();
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: DerivedKeySize));
            response.PasswordSalt = saltBase64;
            response.PasswordHash = hashBase64;
            return response;
        }

       public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] calculatedPasswordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: DerivedKeySize);
            return CryptographicOperations.FixedTimeEquals(calculatedPasswordHash, Convert.FromBase64String(storedHash));
        }
    }
}
