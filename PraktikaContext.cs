using Microsoft.EntityFrameworkCore;
using Praktika.Models;

namespace Praktika;

public partial class PraktikaContext : DbContext
{
    public PraktikaContext()
    {
    }
    public PraktikaContext(DbContextOptions<PraktikaContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DiscontCard> DiscontCards { get; set; }

    public DbSet<InOutProduct> InOutProducts { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderList> OrderLists { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductCore> ProductCores { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\filim\\OneDrive\\Desktop\\PraktikaPhilimonov\\Praktika\\bin\\Debug\\net8.0-windows\\praktika.db\";Foreign Keys = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
