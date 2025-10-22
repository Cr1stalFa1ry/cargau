using Core.Enum;
using db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace db.Configurations;
public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(p => p.Id);

        var permissions = Enum
            .GetValues<Permissions>()
            .Select(r => new PermissionEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });

        builder.HasData(permissions);
    }
}

