using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/orders")]
    public class OrdersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOrderService orderService;

        public OrdersController(IMapper mapper, IOrderService orderService)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderSaveResource orderSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = mapper.Map<OrderSaveResource, Order>(orderSaveResource);

            await orderService.CreateOrder(order);

            return Ok();
        }
    }
}