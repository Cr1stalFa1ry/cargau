using db.Entities;
using db.Configurations;
using Microsoft.EntityFrameworkCore;

namespace db.Context;

public class TuningContext(DbContextOptions<TuningContext> options)
        : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<CarEntity> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfigurations());
        modelBuilder.ApplyConfiguration(new CarConfigurations());
        modelBuilder.ApplyConfiguration(new ServiceConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());

        modelBuilder
            .Entity<OrderEntity>()
            .Property(o => o.Status)
            .HasConversion<string>(); // Хранить как строку в БД

        base.OnModelCreating(modelBuilder);
    }
}
