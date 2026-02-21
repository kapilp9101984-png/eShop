using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IPasswordResetTokens
    {
        Task<List<PasswordResetTokens>> GetAllPasswordResetTokens();
        Task<PasswordResetTokens> GetPasswordResetToken(long ID);
        Task<bool> CreatePasswordResetToken(PasswordResetTokens token);
        Task<PasswordResetTokens> UpdatePasswordResetToken(PasswordResetTokens token);
        Task<bool> DeletePasswordResetToken(long ID);
    }
}
