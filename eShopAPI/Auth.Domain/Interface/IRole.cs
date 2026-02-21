using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IRole
    {
        Task<List<Role>> GetAllRole();
        Task<Role> GetRole(string RoleID);
        Task<bool> CreateRole(Role Role);
        Task<Role> UpdateRole(Role Role);
        Task<bool> DeleteRole(string RoleID);
    }
}
