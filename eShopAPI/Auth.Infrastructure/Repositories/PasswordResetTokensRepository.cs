using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class PasswordResetTokensRepository : IPasswordResetTokens
    {
        private readonly AuthDbContext _db;

        public PasswordResetTokensRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreatePasswordResetToken(PasswordResetTokens token)
        {
            _db.PasswordResetTokens.Add(token);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePasswordResetToken(long ID)
        {
            var entity = await _db.PasswordResetTokens.FindAsync(ID);
            if(entity == null) return false;
            _db.PasswordResetTokens.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<PasswordResetTokens>> GetAllPasswordResetTokens()
        {
            return await _db.PasswordResetTokens.ToListAsync();
        }

        public async Task<PasswordResetTokens> GetPasswordResetToken(long ID)
        {
            return await _db.PasswordResetTokens.FindAsync(ID);
        }

        public async Task<PasswordResetTokens> UpdatePasswordResetToken(PasswordResetTokens token)
        {
            _db.PasswordResetTokens.Update(token);
            await _db.SaveChangesAsync();
            return token;
        }
    }
}
