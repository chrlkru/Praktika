namespace Praktika.Models;

public class Order : EntityBase
{
    public int ProductId { get; set; }

    public int Count { get; set; }

    public double OrderPrice { get; set; }

    public Product Product { get; set; } = null!;
}
