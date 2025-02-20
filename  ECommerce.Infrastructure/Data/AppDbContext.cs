using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2); // 18 basamaklı, 2 ondalık basamaklı decimal
            modelBuilder.Entity<Basket>()
                .Property(b => b.Amount)
                .HasPrecision(18, 2); // 18 basamak, 2 ondalıklı basamak

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }

    public class ApplicationDbContextSeed
    {
        
        public static async Task SeedAsync(AppDbContext context, UserManager<User> userManager)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    Name = "Admin",
                    Surname = "User"
                };

                // Kullanıcıyı oluştur
                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    // Kullanıcı başarıyla oluşturuldu
                    Console.WriteLine("Kullanıcı başarıyla oluşturuldu");
                }
                else
                {
                    // Hata oluştuysa hata mesajlarını loglayabilirsiniz
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Hata: {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Kullanıcı zaten mevcut.");
            }
        }
    }

}