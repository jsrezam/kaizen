using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Extensions;
using Kaizen.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Core
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<Category>> GetCategoriesAsync(CategoryQuery queryObj)
        {
            var result = new QueryResult<Category>();

            var query = context.Categories.Include(c => c.Products).AsQueryable();

            // var query = entites.AsQueryable();
            query = query.ApplyFiltering(queryObj);



            var columnsMap = new Dictionary<string, Expression<Func<Category, object>>>()
            {
                ["name"] = c => c.Name,
                ["description"] = c => c.Description
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}