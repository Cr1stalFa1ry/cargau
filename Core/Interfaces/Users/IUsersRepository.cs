using Core.Models;

namespace Core.Interfaces.Users
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}
