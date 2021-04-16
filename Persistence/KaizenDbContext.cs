using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Persistence
{
    public class KaizenDbContext : IdentityDbContext
    {
        public KaizenDbContext(DbContextOptions<KaizenDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configures one-to-many relationship
            // modelBuilder.Entity<ApplicationUser>()
            //     .HasMany(au => au.Customers)
            //     .WithOne(c => c.ApplicationUser);
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}