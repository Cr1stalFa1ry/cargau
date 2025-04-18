using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(order => order.Id);

        builder
            .HasOne(order => order.Car)
            .WithMany()
            .HasForeignKey(order => order.CarId);

        builder
            .HasMany(order => order.SelectedServices)
            .WithOne();
    }
}
