using System;
using System.Collections.Generic;

namespace Praktika;

public partial class OrderList
{
    public int OrderListId { get; set; }

    public int? UsersId { get; set; }

    public int? OrderId { get; set; }

    public virtual ICollection<DiscontCard> DiscontCards { get; set; } = new List<DiscontCard>();

    public virtual User? Users { get; set; }
}
