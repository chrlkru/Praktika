using System;
using System.Collections.Generic;

namespace Praktika;

public partial class Order
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public string Status { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
