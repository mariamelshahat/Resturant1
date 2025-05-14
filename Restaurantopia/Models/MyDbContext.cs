using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurantopia.Models;
namespace Restaurantopia.Models
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {


    }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure base IdentityDbContext configurations are applied
            base.OnModelCreating(modelBuilder);

            // Seed roles for Identity
            modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Customer",
                     NormalizedName = "customer",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                 }
            );
        }
    }
}
