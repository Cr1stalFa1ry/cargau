using Core.Models;
using Core.Enum;

namespace Core.Interfaces.Users
{
    public interface IUsersService
    {
        Task<User> GetCurrentUser();
        Task<(string, string)> Register(string userName, string email, string password, Roles role);
        Task<(string, string)> Login(string email, string password);
        Task UpdateProfile(Guid id, string newName, string newEmail, Roles role);
        Task<List<User>> GetUsersAsync();
    }
}
