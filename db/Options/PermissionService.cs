using Core.Enum;
using Core.Interfaces.Permissions;
using Core.Interfaces.Users;

namespace db.Options;
public class PermissionService : IPermissionService
{
    private readonly IUsersRepository _userRepository;

    public PermissionService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<HashSet<Permissions>> GetPermissionAsync(Guid userId)
    {
        return default;//_userRepository.GetUserPermissions(userId);
    }
}

