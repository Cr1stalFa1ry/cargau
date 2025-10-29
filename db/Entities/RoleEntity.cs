namespace db.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public List<PermissionEntity> Permissions { get; set; } = [];
    public List<UserEntity> Users { get; set; } = []; // оставлю по итогу, чтобы потом можно было по роли вытаскивать пользователей
}
