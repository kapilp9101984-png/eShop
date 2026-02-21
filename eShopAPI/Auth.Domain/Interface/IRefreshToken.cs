using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IRefreshToken
    {
        Task<List<RefreshToken>> GetAllRefreshToken();
        Task<RefreshToken> GetRefreshToken(int UserID);
        Task<bool> CreateRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken);
        Task<bool> DeleteRefreshToken(long ID);
        Task<bool> Logout(int UserID);
    }
}
