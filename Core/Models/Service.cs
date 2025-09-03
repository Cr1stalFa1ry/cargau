using System;

namespace Core.Models;

public class Service
{
    public Service(Guid id, string nameService, decimal price, string summary)
    {
        Id = id;
        NameService = nameService;
        Price = price;
        Summary = summary;
    }
    public Guid Id { get; set; }
    public string NameService { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public static Service Create(string nameService, decimal price, string summary)
    {
        return new Service(Guid.NewGuid(), nameService, price, summary);
    }

    public static Service Create(Guid id, string nameService, decimal price, string summary)
    {
        return new Service(id, nameService, price, summary);
    }
}
