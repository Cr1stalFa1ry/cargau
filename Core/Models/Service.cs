using System;

namespace Core.Models;

public class Service
{
    public Service(int id, string nameService, decimal price, string summary)
    {
        Id = id;
        NameService = nameService;
        Price = price;
        Summary = summary;
    }
    public int Id { get; set; }
    public string? NameService { get; set; }
    public decimal Price { get; set; }
    public string? Summary { get; set; }

    public static Service Create(int id, string nameService, decimal price, string summary)
    {
        return new Service(id, nameService, price, summary);
    }
}
