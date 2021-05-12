using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Persistence
{
    public class KaizenDbContext : IdentityDbContext<ApplicationUser>
    {
        public KaizenDbContext(DbContextOptions<KaizenDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
            .HasIndex(u => u.CellPhone)
            .IsUnique();
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignDetail> CampaignDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}