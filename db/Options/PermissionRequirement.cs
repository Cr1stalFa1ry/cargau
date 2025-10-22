using Core.Enum;
using Microsoft.AspNetCore.Authorization;

namespace db.Options;
public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(Permissions[] permissions) => Permissions = permissions;

    public Permissions[] Permissions { get; set; } = [];
}

