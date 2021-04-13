using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Kaizen.Extensions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Kaizen.Core
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(KaizenDbContext context) : base(context) { }
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await entities
                    .Include(e => e.Customers)
                    .Include(e => e.Orders)
                    .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<QueryResult<Employee>> GetEmployeesAsync(EmployeeQuery queryObj)
        {
            var result = new QueryResult<Employee>();

            var query = entities
            .Include(e => e.Orders)
            .Include(e => e.Customers)
            .AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Employee, object>>>()
            {
                ["lastName"] = e => e.LastName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }


    }
}