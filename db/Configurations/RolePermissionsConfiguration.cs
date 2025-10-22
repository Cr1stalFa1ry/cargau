using Core.Enum;
using db.Entities;
using db.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace db.Configurations;
public class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _options;
    public RolePermissionsConfiguration(AuthorizationOptions options)
    {
        _options = options;
    }
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(a => new { a.RoleId, a.PermissionId });

        builder.HasOne<RoleEntity>()
            .WithMany()
            .HasForeignKey(r => r.RoleId);

        builder.HasOne<PermissionEntity>()
            .WithMany()
            .HasForeignKey(p => p.PermissionId);

        builder.HasData(ParseRolePermissions());
    }

    private IEnumerable<RolePermissionEntity> ParseRolePermissions()
    {
        return _options.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity()
                {
                    RoleId = (int)Enum.Parse<Roles>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permissions>(p)
                }));
    }
}

