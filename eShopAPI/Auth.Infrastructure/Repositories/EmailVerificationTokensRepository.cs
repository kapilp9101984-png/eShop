using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class EmailVerificationTokensRepository : IEmailVerificationTokens
    {
        private readonly AuthDbContext _db;

        public EmailVerificationTokensRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateEmailVerificationToken(EmailVerificationTokens token)
        {
            _db.EmailVerificationTokens.Add(token);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmailVerificationToken(long ID)
        {
            var entity = await _db.EmailVerificationTokens.FindAsync(ID);
            if(entity == null) return false;
            _db.EmailVerificationTokens.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<EmailVerificationTokens>> GetAllEmailVerificationTokens()
        {
            return await _db.EmailVerificationTokens.ToListAsync();
        }

        public async Task<EmailVerificationTokens> GetEmailVerificationToken(long ID)
        {
            return await _db.EmailVerificationTokens.FindAsync(ID);
        }

        public async Task<EmailVerificationTokens> UpdateEmailVerificationToken(EmailVerificationTokens token)
        {
            _db.EmailVerificationTokens.Update(token);
            await _db.SaveChangesAsync();
            return token;
        }
    }
}
