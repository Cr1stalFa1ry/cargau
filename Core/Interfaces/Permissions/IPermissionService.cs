namespace Core.Interfaces.Permissions;
public interface IPermissionService
{
    Task<HashSet<Enum.Permissions>> GetPermissionAsync(Guid userID);
}
