namespace Praktika.Models;

public class DiscontCard : EntityBase
{
    public int UserId { get; set; }

    public double Discont { get; set; }

    public User User { get; set; } = null!;
}
