using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshToken
    {
        private readonly AuthDbContext _db;

        public RefreshTokenRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateRefreshToken(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRefreshToken(long ID)
        {
            var entity = await _db.RefreshTokens.FindAsync(ID);
            if(entity == null) return false;
            _db.RefreshTokens.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<RefreshToken>> GetAllRefreshToken()
        {
            return await _db.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken> GetRefreshToken(int UserID)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(x => x.UserID == UserID);
        }

        public async Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Update(refreshToken);
            await _db.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<bool> Logout(int UserID)
        {
            var tokens = await _db.RefreshTokens.Where(x => x.UserID == UserID).ToListAsync();
            if(!tokens.Any()) return false;
            foreach(var t in tokens)
            {
                t.IsLogOut = true;
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
