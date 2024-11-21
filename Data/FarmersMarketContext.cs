using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models.Entities;
using FarmersMarketAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FarmersMarketAPI.Utilities;

namespace FarmersMarketAPI.Data
{
    public class FarmersMarketContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FarmersMarketContext(DbContextOptions<FarmersMarketContext> options) : base(options)
        {
        }

        /// TODO: add DbSet properties for each entity
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<MarketOrder> MarketOrders { get; set; }
        public DbSet<Offer> Offers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureProducts(modelBuilder);
            ConfigureFarms(modelBuilder);
            ConfigureOffers(modelBuilder);
            ConfigureOrders(modelBuilder);

            PopulateDb(modelBuilder);
        }

        public void ConfigureFarms(ModelBuilder builder) 
        {
            builder.Entity<Farm>()
                .Property(f => f.FarmSize)
                .HasConversion<string>();

            builder.Entity<Farm>().HasOne(f => f.Farmer).WithMany(f => f.Farms).HasForeignKey(f => f.FarmerId).OnDelete(DeleteBehavior.Cascade);
        }

        public void ConfigureOrders(ModelBuilder builder) 
        {

            builder.Entity<MarketOrder>()
            .HasKey(f => f.Id);

            builder.Entity<MarketOrder>()
            .Property(mo => mo.DeliveryMethod)
            .HasConversion<string>();

            builder.Entity<MarketOrder>()
            .Property(mo => mo.PaymentMethod)
            .HasConversion<string>();

            builder.Entity<MarketOrder>()
                .Property(o => o.OrderStatus)
                .HasConversion<string>();

            builder.Entity<MarketOrder>().HasOne(o => o.Buyer).WithMany(o => o.Orders).HasForeignKey(o => o.BuyerId).OnDelete(DeleteBehavior.Restrict);
        }

        public void ConfigureOffers(ModelBuilder builder) 
        {
            builder.Entity<Offer>()
                .Property(o => o.OfferPrice)
                .HasColumnType("decimal(19, 2)");


            builder.Entity<Offer>()
                .Property(o => o.OfferStatus)
                .HasConversion<string>();

            builder.Entity<Offer>().HasOne(o => o.CreatedBy).WithMany(u => u.CreatedOffers).HasForeignKey(o => o.CreatedById).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Offer>().HasOne(o => o.OfferedTo).WithMany(u => u.ReceivedOffers).HasForeignKey(o => o.OfferedToId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Offer>().HasOne(o => o.Product).WithMany(p => p.Offers).HasForeignKey(o => o.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Offer>().HasOne(o => o.Order).WithMany(o => o.Offers).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Restrict);
        }

        public void ConfigureProducts(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(19, 2)");

            builder.Entity<Product>().HasOne(p => p.Farm).WithMany(f => f.Products).HasForeignKey(p => p.FarmId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }

        private void PopulateDb(ModelBuilder builder)
        {
            //Seeding a  'Administrator' role to AspNetRoles table
            builder.Entity<IdentityRole<Guid>>()
                .HasData(new IdentityRole<Guid> {
                    Id = new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "46477ba0-29d2-4ba5-b10c-471a6aa88869"
                });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            builder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"), // primary key
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FirstName = "Admin",
                    MiddleName = "Admin",
                    LastName = "Admin",
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = PasswordHelper.HashPassword("12345Admin"),
                    ConcurrencyStamp = "598b1fc2-783d-4c22-82e3-0fcc51bc4f16"
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                    UserId = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9")
                }
            );
            
        }
    }
}
 