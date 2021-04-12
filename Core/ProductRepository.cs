using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Kaizen.Core.Models;
using Kaizen.Persistence;
using Kaizen.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Kaizen.Core
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(KaizenDbContext context) : base(context) { }

        public async Task<Product> GetProductAsync(int id)
        {
            return await context.Products
                    .Include(p => p.Category)
                    .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj)
        {
            var result = new QueryResult<Product>();

            var query = context.Products.Include(p => p.Category).AsQueryable();

            // var query = entites.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["category.name"] = p => p.Name
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}