using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IEmailVerificationTokens
    {
        Task<List<EmailVerificationTokens>> GetAllEmailVerificationTokens();
        Task<EmailVerificationTokens> GetEmailVerificationToken(long ID);
        Task<bool> CreateEmailVerificationToken(EmailVerificationTokens token);
        Task<EmailVerificationTokens> UpdateEmailVerificationToken(EmailVerificationTokens token);
        Task<bool> DeleteEmailVerificationToken(long ID);
    }
}
