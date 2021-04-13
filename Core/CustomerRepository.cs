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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(KaizenDbContext context) : base(context) { }
        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await entities
                    .Include(c => c.Employee)
                    .SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj)
        {
            var result = new QueryResult<Customer>();
            var query = entities.Include(c => c.Employee).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Customer, object>>>()
            {
                ["name"] = c => c.LastName,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

    }
}