using System;

namespace db.Entities;

public class ServiceEntity
{
    public int Id { get; set; }
    public string? NameService { get; set; }
    public decimal Price { get; set; }
    public string? Summary { get; set; }
    //public List<OrderEntity> Orders { get; set; } = [];

    // надо добавить сущность заказа, так сказать ссылку на заказ

    // Можно еще добавить время выполнения, 
    // а так конечно нужно проверить про цену, 
    // вроде есть специальный тип для цен
}
