using db.Entities;
using db.Configurations;
using Microsoft.EntityFrameworkCore;

namespace db.Context;

// Экземпляр DbContext представляет сеанс с базой данных и может использоваться для запроса и сохранения экземпляров 
// сущностей. DbContext — это сочетание шаблонов единиц работы и репозитория.

public class TuningContext(DbContextOptions<TuningContext> options)
        : DbContext(options)
{
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
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
        modelBuilder.ApplyConfiguration(new RefreshTokenConfigurations());

        modelBuilder
            .Entity<OrderEntity>()
            .Property(o => o.Status)
            .HasConversion<string>(); // ������� ��� ������ � ��

        base.OnModelCreating(modelBuilder);
    }
}
