using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/customers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomersController(IMapper mapper, ICustomerService customerService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.customerService = customerService;
            this.mapper = mapper;
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateCustomer([FromBody] CustomerResource customerResource)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var customer = mapper.Map<CustomerResource, Customer>(customerResource);

        //     await unitOfWork.CustomerRepository.AddAsync(customer);
        //     await unitOfWork.SaveChangesAsync();
        //     var result = mapper.Map<Customer, CustomerResource>(customer);

        //     return Ok(result);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerResource customerResource)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);

        //     if (customer == null)
        //         return NotFound();

        //     mapper.Map<CustomerResource, Customer>(customerResource, customer);
        //     unitOfWork.CustomerRepository.Update(customer);
        //     await unitOfWork.SaveChangesAsync();
        //     var result = mapper.Map<Customer, CustomerResource>(customer);
        //     return Ok(result);
        // }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(CustomerQueryResource customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryResource, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetCustomersAsync(customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("userCustomers")]
        public async Task<IActionResult> GetUserCustomersAsync(CustomerQueryResource customerQueryResource)
        {
            var userEmail = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Equals("email")).Value;
            var user = await userManager.FindByEmailAsync(userEmail);

            var customerQuery = mapper.Map<CustomerQueryResource, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetUserCustomersAsync(user.Id, customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerResource>>(queryResult);
            return Ok(resultQuery);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetCustomer(int id)
        // {
        //     var customer = await unitOfWork.CustomerRepository.GetCustomerAsync(id);
        //     if (customer == null)
        //         return NotFound();

        //     var customerResource = mapper.Map<Customer, CustomerResource>(customer);
        //     return Ok(customerResource);
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteCustomer(int id)
        // {
        //     var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);
        //     if (customer == null)
        //         return NotFound();
        //     var result = unitOfWork.CustomerRepository.Delete(customer);
        //     await unitOfWork.SaveChangesAsync();
        //     return Ok(result);
        // }
    }
}