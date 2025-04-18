using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class CarConfigurations : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.HasKey(car => car.Id);
    }
}
