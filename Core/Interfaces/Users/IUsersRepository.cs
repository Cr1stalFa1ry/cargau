using Core.Models;

namespace Core.Interfaces.Users
{
    public interface IUsersRepository
    {
        Task<User> GetUser(Guid userId);
        Task<List<User>> GetUsers();
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task Update(User updateUser);
        //Task<HashSet<Enum.Permissions>> GetUserPermissions(Guid userId);
    }
}
