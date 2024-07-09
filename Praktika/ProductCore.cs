using System;
using System.Collections.Generic;

namespace Praktika;

public partial class ProductCore
{
    public int ProductCoreId { get; set; }

    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public string ColorOptions { get; set; } = null!;

    public string SizeOptions { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
