using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserInRoleRepository : IUserInRole
    {
        private readonly AuthDbContext _db;

        public UserInRoleRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AssignUserRole(int UserID, List<int> RoleID)
        {
            foreach(var r in RoleID)
            {
                _db.UserInRoles.Add(new UserInRole { UserID = UserID, RoleID = r });
            }
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserInRole>> GetAllUserInRole()
        {
            return await _db.UserInRoles.ToListAsync();
        }

        public async Task<Role> GetRoleOfUser(int UserID)
        {
            var rel = await _db.UserInRoles.FirstOrDefaultAsync(x => x.UserID == UserID);
            if(rel == null) return null;
            return await _db.Roles.FindAsync(rel.RoleID);
        }

        public async Task<User> GetUserInRole(int RoleID)
        {
            var rel = await _db.UserInRoles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
            if(rel == null) return null;
            return await _db.Users.FindAsync(rel.UserID);
        }

        public async Task<User> RemoveUserRole(int UserID, int RoleID)
        {
            var rel = await _db.UserInRoles.FirstOrDefaultAsync(x => x.UserID == UserID && x.RoleID == RoleID);
            if(rel == null) return null;
            _db.UserInRoles.Remove(rel);
            await _db.SaveChangesAsync();
            return await _db.Users.FindAsync(UserID);
        }

        public async Task<bool> DeactivateRole(string UserID, List<int> RoleID)
        {
            if(!int.TryParse(UserID, out var uid)) return false;
            foreach(var r in RoleID)
            {
                var rel = await _db.UserInRoles.FirstOrDefaultAsync(x => x.UserID == uid && x.RoleID == r);
                if(rel != null) _db.UserInRoles.Remove(rel);
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
