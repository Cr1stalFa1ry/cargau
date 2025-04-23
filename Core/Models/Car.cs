using System;

namespace Core.Models;

public class Car
{
    public Car(Guid id, string brand, string model, string release, Guid ownerId, decimal price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Price = price;
        YearRelease = release;
        OwnerId = ownerId;
    }

    public Car(Guid ownerId, decimal price)
    {
        Price = price;
        OwnerId = ownerId;
    }

    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public string YearRelease { get; } = string.Empty;
    public Guid OwnerId { get; }
    public decimal Price { get; private set; }

    public static Car Create(Guid id, string brand, string model, string yearRelease, Guid ownerId, decimal price)
    {
        return new Car(id, brand, model, yearRelease, ownerId, price);
    }
    public static Car Create(Guid ownerId, decimal price)
    {
        return new Car(ownerId, price);
    }
}
