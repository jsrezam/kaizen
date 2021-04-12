using Kaizen.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Persistence
{
    public class KaizenDbContext : DbContext
    {
        public KaizenDbContext(DbContextOptions<KaizenDbContext> options)
        : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}