using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/customers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;
        private readonly IUserService userService;

        public CustomersController(IMapper mapper, ICustomerService customerService, IUserService userService)
        {
            this.userService = userService;
            this.customerService = customerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync(CustomerQueryDto customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryDto, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetCustomersAsync(customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultDto<CustomerDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("userCustomers")]
        public async Task<IActionResult> GetAgentCustomersAsync(CustomerQueryDto customerQueryResource)
        {
            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            var customerQuery = mapper.Map<CustomerQueryDto, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetAgentCustomersAsync(agent.Id, customerQuery);
            var resultQuery = mapper.Map<QueryResult<AgentCustomer>, QueryResultDto<AgentCustomerDto>>(queryResult);
            return Ok(resultQuery);
        }
    }
}