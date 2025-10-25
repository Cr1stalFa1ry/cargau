using System.Text.RegularExpressions;
using Core.Enum;

namespace Core.Models;

public class User
{
    public User(Guid id, Roles role, string userName, string email, string passwordHash)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
    }

    public Guid Id { get; private set; }
    public Roles Role { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; } 

    public static User Create(Guid id, Roles role, string userName, string email, string passwordHash = "password")
    {        
        return new User(id, role, userName, email, passwordHash);
    }
}
