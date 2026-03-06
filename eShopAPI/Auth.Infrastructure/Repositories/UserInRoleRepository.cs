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

        public async Task<List<Role>> GetRoleOfUser(int UserID)
        {
            var rel = _db.UserInRoles.Where(x => x.UserID == UserID && x.IsDeleted == false && x.IsActive).ToList();
            if (rel.Count() > 0) { 
                var roleIDs = rel.Select(x => x.RoleID).ToList();
                return _db.Roles.Where(x => roleIDs.Contains(x.ID) && x.IsDeleted == false && x.IsActive).ToList();
            }            
            return new List<Role>();
        }

        public async Task<List<User>> GetUserInRole(int RoleID)
        {
            var rel = _db.UserInRoles.Where(x => x.RoleID == RoleID && x.IsDeleted == false && x.IsActive).ToList();
            if (rel.Count() > 0)
            {
                var userIds = rel.Select(x => x.UserID).ToList();
                return _db.Users.Where(x => userIds.Contains(x.ID)).ToList();
            }
            return new List<User>();
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
