using Core.Models;

namespace Core.Interfaces.Users
{
    public interface IUsersService
    {
        Task<(string, string)> Register(string userName, string email, string password);
        Task<(string, string)> Login(string email, string password);
        Task UpdateProfile(Guid id, string newName, string newEmail);
        //Task<List<User>> GetUsersAsync();
    }
}
