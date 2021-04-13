using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/customers")]
    public class CustomersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerResource customerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = mapper.Map<CustomerResource, Customer>(customerResource);

            await unitOfWork.CustomerRepository.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Customer, CustomerResource>(customer);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerResource customerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            mapper.Map<CustomerResource, Customer>(customerResource, customer);
            unitOfWork.CustomerRepository.Update(customer);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Customer, CustomerResource>(customer);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(CustomerQueryResource customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryResource, CustomerQuery>(customerQueryResource);
            var queryResult = await unitOfWork.CustomerRepository.GetCustomersAsync(customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await unitOfWork.CustomerRepository.GetCustomerAsync(id);
            if (customer == null)
                return NotFound();

            var customerResource = mapper.Map<Customer, CustomerResource>(customer);
            return Ok(customerResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();
            var result = unitOfWork.CustomerRepository.Delete(customer);
            await unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
    }
}