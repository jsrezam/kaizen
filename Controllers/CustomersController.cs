using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync(CustomerQueryResource customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryResource, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetCustomersAsync(customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("userCustomers")]
        public async Task<IActionResult> GetAgentCustomersAsync(CustomerQueryResource customerQueryResource)
        {
            var userEmail = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Equals("email")).Value;
            var user = await userManager.FindByEmailAsync(userEmail);

            var customerQuery = mapper.Map<CustomerQueryResource, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetAgentCustomersAsync(user.Id, customerQuery);
            var resultQuery = mapper.Map<QueryResult<AgentCustomer>, QueryResultResource<AgentCustomerResource>>(queryResult);
            return Ok(resultQuery);
        }
    }
}