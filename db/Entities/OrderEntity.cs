using System;

namespace db.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public string? Client { get; set; } // может вместо строки сделать модель User, тут уже добавится еще UserId как внешний ключ
    public Guid CarId { get; set; }
    public CarEntity? Car { get; set; } // не знаю даже как избавиться от предупреждения про non-nullable
    public List<ServiceEntity> SelectedServices { get; set; } = [];
    public string? Status { get; set; }
    public DateTime Date { get; set; }
}
