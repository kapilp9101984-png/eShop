using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly AuthDbContext _db;

        public RoleRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateRole(Role Role)
        {
            _db.Roles.Add(Role);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRole(string RoleID)
        {
            if(!int.TryParse(RoleID, out var id)) return false;
            var entity = await _db.Roles.FindAsync(id);
            if(entity == null) return false;
            _db.Roles.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Role> GetRole(string RoleID)
        {
            if(!int.TryParse(RoleID, out var id)) return null;
            return await _db.Roles.FindAsync(id);
        }

        public async Task<Role> UpdateRole(Role Role)
        {
            _db.Roles.Update(Role);
            await _db.SaveChangesAsync();
            return Role;
        }
    }
}
