using Core.Models;
using Core.Interfaces.Users;
using Core.Enum;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace db.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TuningContext _dbContext;
    private readonly IMapper _mapper;
 
    public UsersRepository(TuningContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<User> GetUser(Guid userId)
    {
        var userEntity = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
            throw new ArgumentNullException("user not found");

        return _mapper.Map<User>(userEntity);
    }

    public async Task<List<User>> GetUsers()
    {
        var usersEntities = await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .ToListAsync();

        var users = usersEntities
            .Select(user => _mapper.Map<User>(user))
            .ToList();

        return users;
    }

    public async Task Add(User user)
    {
        // получаем роль из БД, заодно проверяя что такая роль существует
        var roleEntity = await _dbContext.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)user.Role)
            ?? throw new InvalidOperationException("role not found");

        var userEntity = _mapper.Map<UserEntity>(user);
        userEntity.Role = roleEntity;

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentNullException("user not found");

        return _mapper.Map<User>(userEntity);
    }

    public async Task Update(User updateUser)
    {
        var res = await _dbContext.Users
            .Where(u => u.Id == updateUser.Id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.UserName, updateUser.UserName)
                .SetProperty(u => u.Email, updateUser.Email)
                .SetProperty(u => u.RoleId, (int) updateUser.Role)
            );

        if (res == 0)
            throw new ArgumentNullException("user not found");
    }
}
