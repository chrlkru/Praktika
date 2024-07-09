using System;
using System.Collections.Generic;

namespace Praktika;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductCoreId { get; set; }

    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int Count { get; set; }

    public virtual ICollection<InOutProduct> InOutProducts { get; set; } = new List<InOutProduct>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ProductCore ProductCore { get; set; } = null!;
}
