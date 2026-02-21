using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IUserInRole
    {
        Task<List<UserInRole>> GetAllUserInRole();
        Task<User> GetUserInRole(int RoleID);
        Task<Role> GetRoleOfUser(int UserID);
        Task<bool> AssignUserRole(int UserID, List<int> RoleID);
        Task<User> RemoveUserRole(int UserID, int RoleID);
        Task<bool> DeactivateRole(string UserID, List<int> RoleID);
    }
}
