using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Extensions;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(KaizenDbContext context) : base(context) { }

        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            var customer = await entities
            .SingleOrDefaultAsync(c => c.CellPhone.Equals(cellPhone));
            return (customer != null) ? true : false;
        }

        public async Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj)
        {
            var result = new QueryResult<Customer>();
            var query = entities.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Customer, object>>>()
            {
                ["firstName"] = c => c.FirstName,
                ["lastName"] = c => c.LastName,
                ["identificationCard"] = c => c.IdentificationCard,
                ["email"] = c => c.Email,
                ["cellPhone"] = c => c.CellPhone,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}