namespace db.Entities
{
    public class UserEntity //: IdentityUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<OrderEntity?> Orders { get; set; } = [];

        // Другие свойства ...
    }
}
