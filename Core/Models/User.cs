using Core.Enum;

namespace Core.Models;

public class User
{
    public User(Guid id, string userName, string passwordHash, string email, Roles role = Roles.User)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
    }

    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; } 
    public string Email { get; private set; }
    public Roles Role { get; private set; }

    public static User Create(Guid id, string userName, string passwordHash, string email, Roles role)
    {
        return new User(id, userName, passwordHash, email, role);
    }

    public static User Create(Guid id, string userName, string email, Roles role)
    {
        return new User(id, userName, "passwordHash", email, role);
    }
}
