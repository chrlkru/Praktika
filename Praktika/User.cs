using System;
using System.Collections.Generic;

namespace Praktika;

public partial class User
{
    public int UsersId { get; set; }

    public string Фио { get; set; } = null!;

    public string Телефон { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime ДатаРождения { get; set; }

    public virtual ICollection<DiscontCard> DiscontCards { get; set; } = new List<DiscontCard>();

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();
}
