using Core.Enum;
using db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace db.Configurations;
public class RoleConfigurations : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        //builder.HasMany(r => r.Permissions) // излишне, так как сам создал join таблицу RolePermission
        // из за этого дублировались столбы RoleId PermissionId
        //    .WithMany(p => p.Roles)
        //    .UsingEntity<RolePermissionEntity>(j => j.ToTable("RolePermissions"));

        var roles = Enum
            .GetValues<Roles>()
            .Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });

        builder.HasData(roles);
    }
}

