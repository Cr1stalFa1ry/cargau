using Core.Models;
using Core.Interfaces.Users;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Enum;

namespace db.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TuningContext _dbContext;
 
    public UsersRepository(TuningContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUser(Guid userId)
    {
        var userEntity = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
            throw new ArgumentNullException("user not found");

        var role = (Roles)userEntity.Role!.Id;

        return User.Create(userId,
                           userEntity!.UserName,
                           userEntity.Email,
                           role);
    }

    public async Task<List<User>> GetUsers()
    {
        var usersEntities = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        var users = usersEntities
            .Select(user => User
                .Create(user.Id,
                        user.UserName,
                        user.Email,
                        (Roles)user.Role!.Id) // предполагается, что у пользователя есть хотя бы одна роль, как будто бы костыль
            ) 
            .ToList();

        return users;
    }

    public async Task Add(User user)
    {
        // захардкодили, чтобы проверить разрешения в работе
        var roleEntity = await _dbContext.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)user.Role)
            ?? throw new InvalidOperationException("role not found");

        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            Role = roleEntity,
            RoleId = roleEntity.Id
        };

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentNullException("user not found");

        var role = (Roles)userEntity.Role!.Id;

        var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email, role);

        return user;
        //_mapper.Map<User>(userEntity);
    }

    public async Task Update(User updateUser)
    {
        var res = await _dbContext.Users
            .Where(u => u.Id == updateUser.Id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.UserName, updateUser.UserName)
                .SetProperty(u => u.Email, updateUser.Email)
            );

        if (res == 0)
            throw new Exception("user not found");
    }

    // Вытаскиваем все разрешения пользователя
    // public async Task<HashSet<Permissions>> GetUserPermissions(Guid userId)
    // {
    //     var roles = await _dbContext.Users
    //         .AsNoTracking()
    //         .Include(u => u.Roles)
    //         .ThenInclude(r => r.Permissions)
    //         .Where(u => u.Id == userId)
    //         .Select(u => u.Roles)
    //         .ToListAsync();

    //     return roles
    //         .SelectMany(r => r)
    //         .SelectMany(r => r.Permissions)
    //         .Select(p => (Permissions)p.Id)
    //         .ToHashSet();
    // }
}
