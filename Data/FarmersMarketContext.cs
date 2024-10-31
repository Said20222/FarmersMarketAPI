using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models;

namespace FarmersMarketAPI.Data
{
    public class FarmersMarketContext : DbContext
    {
        public FarmersMarketContext(DbContextOptions<FarmersMarketContext> options) : base(options)
        {
        }

        /// TODO: add DbSet properties for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farm> Farms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure relationships, constraints, or table names if different from class names.
            // e.g., modelBuilder.Entity<User>().ToTable("MarketUser");
        }
    }
}
