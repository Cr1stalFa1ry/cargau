using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class CarConfigurations : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder
            .HasKey(car => car.Id);

        builder
            .HasOne(car => car.Owner)
            .WithMany(owner => owner.Cars)
            .HasForeignKey(car => car.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
