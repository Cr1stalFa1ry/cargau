using System;

namespace Core.Models;

public class Car
{
    public Car(Guid id, string brand, string model, string owner, decimal price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Owner = owner;
        Price = price;
    }

    public Guid Id { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Owner { get; private set; }
    public decimal Price { get; private set; }

    public static Car Create(Guid id, string brand, string model, string owner, decimal price)
    {
        return new Car(id, brand, model, owner, price);
    }
}
