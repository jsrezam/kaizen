using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/orders")]
    public class OrdersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public OrdersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = mapper.Map<OrderResource, Order>(orderResource);

            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Order, OrderResource>(order);

            return Ok(result);
        }


    }
}