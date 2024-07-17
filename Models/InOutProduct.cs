namespace Praktika.Models;

public class InOutProduct : EntityBase
{
    public int ProductId { get; set; }

    public int Count { get; set; }

    public Product Product { get; set; } = null!;
}
