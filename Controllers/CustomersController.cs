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

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await customerService.isUniqueCellphone(customerDto.CellPhone);

            if (!response)
                return BadRequest("The cellphone is already taken by another customer or agent");

            customerDto = await customerService.GetLocationNames(customerDto);

            var customer = mapper.Map<CustomerDto, Customer>(customerDto);
            await customerService.CreateCustomer(customer);

            return Ok();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerAsync(int customerId)
        {
            var customer = await customerService.GetCustomerAsync(customerId);

            if (customer == null)
                return NotFound();

            var cutomerDto = mapper.Map<Customer, CustomerDto>(customer);
            cutomerDto = await customerService.GetLocationIds(cutomerDto);

            return Ok(cutomerDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync(CustomerQueryDto customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryDto, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetCustomersAsync(customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultDto<CustomerDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("availables/{agentId}")]
        public async Task<IActionResult> GetAgentAvailableCustomersAsync(string agentId)
        {
            var queryResult = await customerService.GetAgentAvailableCustomersAsync(agentId);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultDto<CustomerDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("noInCampaign/{campaignId}")]
        public async Task<IActionResult> GetNoInCampaignCustomersAsync(int campaignId, CustomerQueryDto customerQueryResource)
        {
            var customerQuery = mapper.Map<CustomerQueryDto, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetNoInCampaignCustomersAsync(campaignId, customerQuery);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultDto<CustomerDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("agentCustomers")]
        public async Task<IActionResult> GetAgentCustomersAsync(CustomerQueryDto customerQueryResource)
        {
            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            if (agent == null) return NotFound();

            var customerQuery = mapper.Map<CustomerQueryDto, CustomerQuery>(customerQueryResource);
            var queryResult = await customerService.GetAgentCustomersAsync(agent.Id, customerQuery);
            var resultQuery = mapper.Map<QueryResult<AgentCustomerViewModel>, QueryResultDto<AgentCustomerViewModelDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomerAsync(int customerId, [FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = await customerService.GetCustomerAsync(customerId);

            if (customer == null)
                return NotFound();

            if (!customer.CellPhone.Equals(customerDto.CellPhone))
            {
                var response = await customerService.isUniqueCellphone(customerDto.CellPhone);

                if (!response)
                    return BadRequest("The cellphone is already taken by another customer or agent");
            }

            customerDto = await customerService.GetLocationNames(customerDto);

            mapper.Map<CustomerDto, Customer>(customerDto, customer);
            await customerService.UpdateCustomerAsync(customer);

            var cutomerDto = mapper.Map<Customer, CustomerDto>(customer);
            cutomerDto = await customerService.GetLocationIds(cutomerDto);

            return Ok(cutomerDto);

        }

        [HttpGet("random/{maxRange}")]
        public async Task<IActionResult> GetRandomCustomersAsync(int maxRange, ApplicationUserQueryDto applicationUserQueryDto)
        {
            var queryResult = await customerService.GetRandomCustomersAsync(maxRange, applicationUserQueryDto);
            var resultQuery = mapper.Map<QueryResult<Customer>, QueryResultDto<CustomerDto>>(queryResult);
            return Ok(resultQuery);
        }
    }
}