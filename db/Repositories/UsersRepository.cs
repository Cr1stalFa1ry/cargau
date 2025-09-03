using Core.Models;
using Core.Interfaces.Users;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;

namespace db.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TuningContext _dbContext;
 
    public UsersRepository(TuningContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> Get()
    {
        var usersEntities = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        var users = usersEntities
            .Select(user => User.Create(user.Id, user.UserName, user.Email))
            .ToList();

        return users;
    }

    public async Task Add(User user)
    {
        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email
        };

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentNullException("user not found");

        var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);

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
}
