using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/employees")]
    public class EmployeesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public EmployeesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeResource employeeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = mapper.Map<EmployeeResource, Employee>(employeeResource);

            await unitOfWork.EmployeeRepository.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Employee, EmployeeResource>(employee);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeResource employeeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            mapper.Map<EmployeeResource, Employee>(employeeResource, employee);
            unitOfWork.EmployeeRepository.Update(employee);
            await unitOfWork.SaveChangesAsync();

            var result = mapper.Map<Employee, EmployeeResource>(employee);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(EmployeeQueryResource employeeQueryResource)
        {
            var employeeQuery = mapper.Map<EmployeeQueryResource, EmployeeQuery>(employeeQueryResource);
            var queryResult = await unitOfWork.EmployeeRepository.GetEmployeesAsync(employeeQuery);
            var resultQuery = mapper.Map<QueryResult<Employee>, QueryResultResource<EmployeeResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetEmployeeAsync(id);
            if (employee == null)
                return NotFound();

            var employeeResource = mapper.Map<Employee, EmployeeResource>(employee);
            return Ok(employeeResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            var result = unitOfWork.EmployeeRepository.Delete(employee);
            await unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
    }
}