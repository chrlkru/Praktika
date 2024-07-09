using System;
using System.Collections.Generic;

namespace Praktika;

public partial class InOutProduct
{
    public int InOutProductId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public virtual Product Product { get; set; } = null!;
}
