using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Core.Services;
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
            await orderService.SaveOrderDetail(order.Id, orderSaveResource.OrderDetails);
            return Ok();
        }
    }
}