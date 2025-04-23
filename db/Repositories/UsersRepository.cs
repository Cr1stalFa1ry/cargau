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
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentException("user not found");

        var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);

        return user;
        //_mapper.Map<User>(userEntity);
    }
}
