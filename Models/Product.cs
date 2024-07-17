namespace Praktika.Models;

public class Product : EntityBase
{
    public int ProductCoreId { get; set; }

    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int Count { get; set; }

    public List<InOutProduct> InOutProducts { get; set; } = null!;

    public ProductCore ProductCore { get; set; } = null!;
}
