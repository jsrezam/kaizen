using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Common;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/orders")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOrderService orderService;

        private readonly IUserService userService;

        public OrdersController(IMapper mapper, IOrderService orderService, IUserService userService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderSaveDto orderSaveDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = mapper.Map<OrderSaveDto, Order>(orderSaveDto);

            await orderService.CreateOrderAsync(order);

            return Ok();
        }

        [HttpGet("{agentId}")]
        [Authorize(Policies.AdminRoleValue)]
        public async Task<IActionResult> GetAgentOrdersByAgentIdAsync(string agentId, OrderQueryDto orderQueryDto)
        {
            var orderQuery = mapper.Map<OrderQueryDto, OrderQuery>(orderQueryDto);
            var orders = await orderService.GetAgentOrdersAsync(agentId, orderQuery);
            var ordersDto = mapper.Map<QueryResult<Order>, QueryResultDto<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAgentOrdersAsync(OrderQueryDto orderQueryDto)
        {
            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            var orderQuery = mapper.Map<OrderQueryDto, OrderQuery>(orderQueryDto);
            var orders = await orderService.GetAgentOrdersAsync(agent.Id, orderQuery);
            var ordersDto = mapper.Map<QueryResult<Order>, QueryResultDto<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpGet("orderDetail/{orderId}")]
        public async Task<IActionResult> GetOrderDetailAsync(int orderId)
        {
            var orderDetail = await orderService.GetOrderDetailAsync(orderId);
            var orderDetailDto = mapper.Map<OrderViewModel, OrderViewModelDto>(orderDetail);

            return Ok(orderDetailDto);
        }
    }
}