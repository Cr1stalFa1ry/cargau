namespace db.Entities;
public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public List<RoleEntity> Roles { get; set; } = [];
}

