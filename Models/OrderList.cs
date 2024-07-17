namespace Praktika.Models;

public class OrderList : EntityBase
{
    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public double OrderListPrice { get; set; }

    public List<Order> Orders { get; set; } = null!;

    public User User { get; set; } = null!;
}
