using Auth.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Interface
{
    public interface IFeatureInRole
    {
        public Task<List<IFeatureInRole>> GetAllFeatureInRole();
        public Task<Features> GetAllFeatureInRole(int RoleID);
        public Task<Role> GetAllRollOfFeature(int FeatureID);
        public Task<bool> AssignFeaturesInRole(int RoleID, List<int> FeatureID);
        public Task<bool> AssignRolesInFeature(int FeatureID, List<int> RoleID);
        public Task<bool> RemoveFeatureInRole(int RoleID, int FeatureID);
        public Task<bool> RemoveFeaturesInRole(int RoleID, List<int> FeatureID);
        public Task<bool> RemoveRolesInFeature(int FeatureID, List<int> RoleID);

    }
}
