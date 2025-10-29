using Core.Enum;

namespace Core.Models;

public class Service
{
    public Service(string nameService, decimal price, string summary, TypeTuning type, int id = 0)
    {
        Name = nameService;
        Price = price;
        Summary = summary;
        Id = id;
        Type = type;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TypeTuning Type { get; set; }
}
