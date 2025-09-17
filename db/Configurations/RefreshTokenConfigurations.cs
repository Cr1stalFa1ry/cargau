using db.Entities;
using Microsoft.EntityFrameworkCore;

namespace db.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.HasOne(rt => rt.User)
               .WithMany(rt => rt.RefreshTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(rt => rt.Token).HasMaxLength(200);

        builder.HasIndex(rt => rt.Token).IsUnique(); // быстрый поиск по токену и уникальность refresh-токена в БД

    }
}
