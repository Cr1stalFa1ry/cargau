namespace Core.Interfaces.Users;

public interface IUserContextService
{
    Guid? GetCurrentUserId();
    string GetCurrentUserEmail();
}

