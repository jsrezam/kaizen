using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<QueryResult<Employee>> GetEmployeesAsync(EmployeeQuery queryObj);
    }
}