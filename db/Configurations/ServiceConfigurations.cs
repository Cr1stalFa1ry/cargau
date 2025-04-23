using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

public class ServiceConfigurations : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder
            .HasKey(service => service.Id);

        builder.Property(service => service.Name)
           .HasMaxLength(100)
           .IsRequired();

        builder.Property(service => service.Summary)
            .HasMaxLength(500);

        builder.Property(service => service.Price)
            .HasColumnType("decimal(18,2)");
    }
}
