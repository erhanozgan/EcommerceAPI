using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Örnek olarak Amount kolonunun type'ını belirtiyoruz
        modelBuilder.Entity<Product>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        // Diğer entity konfigürasyonlarını buraya ekleyebilirsin.
    }
}