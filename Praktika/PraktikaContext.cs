using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Praktika;

public partial class PraktikaContext : DbContext
{
    public PraktikaContext()
    {
    }

    public PraktikaContext(DbContextOptions<PraktikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiscontCard> DiscontCards { get; set; }

    public virtual DbSet<InOutProduct> InOutProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderList> OrderLists { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCore> ProductCores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source= C:\\\\\\\\Data Base\\\\\\\\praktika.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiscontCard>(entity =>
        {
            entity.ToTable("DiscontCard");

            //entity.HasOne(d => d.OrderList).WithMany(p => p.DiscontCards)
            //    .HasForeignKey(d => d.OrderListId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Users).WithMany(p => p.DiscontCards)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<InOutProduct>(entity =>
        {
            entity.ToTable("InOutProduct");

            entity.Property(e => e.InOutProductId)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("InOutProductID");
            entity.Property(e => e.Count).HasColumnType("INT");
            entity.Property(e => e.ProductId)
                .HasColumnType("INT")
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.InOutProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderList>(entity =>
        {
            entity.ToTable("OrderList");

            entity.Property(e => e.OrderListId).ValueGeneratedNever();

            entity.HasOne(d => d.Users).WithMany(p => p.OrderLists).HasForeignKey(d => d.UsersId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductCoreId).HasColumnName("ProductCoreID");

            entity.HasOne(d => d.ProductCore).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductCore>(entity =>
        {
            entity.ToTable("ProductCore");

            entity.Property(e => e.ProductCoreId).HasColumnName("ProductCoreID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId);

            entity.Property(e => e.UsersId).ValueGeneratedNever();
            entity.Property(e => e.ДатаРождения)
                .HasColumnType("DATE")
                .HasColumnName("Дата_рождения");
            entity.Property(e => e.Фио).HasColumnName("ФИО");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
