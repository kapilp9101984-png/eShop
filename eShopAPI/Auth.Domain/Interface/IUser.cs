using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IUser
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUser(string UserID);
        Task<bool> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(string UserID);
    }
}
