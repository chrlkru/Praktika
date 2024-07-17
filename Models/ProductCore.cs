namespace Praktika.Models;

public class ProductCore : EntityBase
{
    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public string ColorOptions { get; set; } = null!;

    public string SizeOptions { get; set; } = null!;

    public List<Product> Products { get; set; } = null!;
}
