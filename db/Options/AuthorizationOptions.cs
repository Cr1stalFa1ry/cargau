namespace db.Options;
public class AuthorizationOptions
{
    public RolePermissions[] RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = String.Empty;
    public string[] Permissions { get; set; } = [];
}

