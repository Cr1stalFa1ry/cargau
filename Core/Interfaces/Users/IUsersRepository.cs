using Core.Models;

namespace Core.Interfaces.Users
{
    public interface IUsersRepository
    {
        Task<List<User>> Get();
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task Update(User updateUser);
    }
}
