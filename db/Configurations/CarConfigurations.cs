using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using db.Entities;

namespace db.Configurations;

// конфигурации нужны для подстраховки себя и не только: можно назначать определенные 
// ограничения на поля сущности (максимальная длина строки), также чтобы еще не вылетало ошибок при удалении каскадом

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

        // OnDelete(DeleteBehavior.Cascade) - при удалении владельца, удаляются все его машины
        // OnDelete(DeleteBehavior.SetNull) - при удалении владельца, у машин устанавливается null в поле OwnerId
        // OnDelete(DeleteBehavior.Restrict) - запретить удаление владельца, если у него есть машины
    }
}
