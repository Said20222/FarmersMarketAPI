using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models.Entities;
using FarmersMarketAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;

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

            modelBuilder.Entity<Farm>().HasOne(f => f.Farmer).WithMany(f => f.Farms).HasForeignKey(f => f.FarmerId);
            modelBuilder.Entity<Product>().HasOne(p => p.Farm).WithMany(f => f.Products).HasForeignKey(p => p.FarmId);
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Offer>().HasOne(o => o.CreatedBy).WithMany(u => u.Offers).HasForeignKey(o => o.CreatedById);
            modelBuilder.Entity<Offer>().HasOne(o => o.OfferedTo).WithMany(u => u.Offers).HasForeignKey(o => o.OfferedToId);
            modelBuilder.Entity<Offer>().HasOne(o => o.Product).WithMany(p => p.Offers).HasForeignKey(o => o.ProductId);
            modelBuilder.Entity<Offer>().HasOne(o => o.Order).WithMany(o => o.Offers).HasForeignKey(o => o.OrderId);
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
                    PasswordHash = "AQAAAAEAACcQAAAAEFvpwHR0kH1sy6DQWpIndLCdmZsahrddpi9XzA5DkTSadoKfzl+amp9ya+lWuMpIWQ==",
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
 