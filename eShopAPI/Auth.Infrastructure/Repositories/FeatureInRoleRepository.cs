using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class FeatureInRoleRepository : IFeatureInRole
    {
        private readonly AuthDbContext _db;

        public FeatureInRoleRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AssignFeaturesInRole(int RoleID, List<int> FeatureID)
        {
            foreach(var f in FeatureID)
            {
                _db.FeatureInRoles.Add(new FeatureInRole { RoleID = RoleID, FeatureID = f });
            }
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignRolesInFeature(int FeatureID, List<int> RoleID)
        {
            foreach(var r in RoleID)
            {
                _db.FeatureInRoles.Add(new FeatureInRole { RoleID = r, FeatureID = FeatureID });
            }
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<IFeatureInRole>> GetAllFeatureInRole()
        {
            // The interface signature is odd; return empty list of interface
            return new List<IFeatureInRole>();
        }

        public async Task<Features> GetAllFeatureInRole(int RoleID)
        {
            var rel = await _db.FeatureInRoles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
            if(rel == null) return null;
            return await _db.Features.FindAsync(rel.FeatureID);
        }

        public async Task<Role> GetAllRollOfFeature(int FeatureID)
        {
            var rel = await _db.FeatureInRoles.FirstOrDefaultAsync(x => x.FeatureID == FeatureID);
            if(rel == null) return null;
            return await _db.Roles.FindAsync(rel.RoleID);
        }

        public async Task<bool> RemoveFeatureInRole(int RoleID, int FeatureID)
        {
            var rel = await _db.FeatureInRoles.FirstOrDefaultAsync(x => x.RoleID == RoleID && x.FeatureID == FeatureID);
            if(rel == null) return false;
            _db.FeatureInRoles.Remove(rel);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFeaturesInRole(int RoleID, List<int> FeatureID)
        {
            foreach(var f in FeatureID)
            {
                var rel = await _db.FeatureInRoles.FirstOrDefaultAsync(x => x.RoleID == RoleID && x.FeatureID == f);
                if(rel != null) _db.FeatureInRoles.Remove(rel);
            }
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRolesInFeature(int FeatureID, List<int> RoleID)
        {
            foreach(var r in RoleID)
            {
                var rel = await _db.FeatureInRoles.FirstOrDefaultAsync(x => x.FeatureID == FeatureID && x.RoleID == r);
                if(rel != null) _db.FeatureInRoles.Remove(rel);
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
