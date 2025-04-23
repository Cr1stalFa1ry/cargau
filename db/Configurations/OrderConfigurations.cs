using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .HasKey(order => order.Id);

        builder
            .HasOne<UserEntity>(order => order.Client)
            .WithMany(client => client.Orders)
            .HasForeignKey(order => order.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CarEntity>(order => order.Car)
            .WithMany(car => car.Orders)
            .HasForeignKey(order => order.CarId);

        builder
            .HasMany(order => order.SelectedServices)
            .WithMany(service => service.Orders);
    }
}
