using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;

public class NightTechDbContext(DbContextOptions<NightTechDbContext> options)
    : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<Cart> Carts { get; set; }
    internal DbSet<CartItem> CartItems { get; set; }
    internal DbSet<Order> Orders { get; set; }
    internal DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category → Product relationship
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // User → Cart relationship
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.Cart)
                  .WithOne(c => c.User)
                  .HasForeignKey<Cart>(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // CartItem → Cart relationship
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasOne(ci => ci.Cart)
                  .WithMany(c => c.Items)
                  .HasForeignKey(ci => ci.CartId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderItem → Order relationship
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(oi => oi.Order)
                  .WithMany(o => o.Items)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Order → User relationship
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(o => o.User)
                  .WithMany(u => u.Orders)
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // 🧮 Decimal precision fixes
        modelBuilder.Entity<Cart>()
            .Property(c => c.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CartItem>()
            .Property(ci => ci.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}
