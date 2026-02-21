using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AuthDbContext _db;

        public UserRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateUser(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string UserID)
        {
            if (!int.TryParse(UserID, out var id)) return false;
            var entity = await _db.Users.FindAsync(id);
            if (entity == null) return false;
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetUser(string UserID)
        {
            if (!int.TryParse(UserID, out var id)) return null;
            return await _db.Users.FindAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
