using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .HasMany<OrderEntity>(user => user.Orders)
            .WithOne(order => order.Client)
            .HasForeignKey(order => order.ClientId);

        builder
            .HasMany<CarEntity>(user => user.Cars);
    }
}
