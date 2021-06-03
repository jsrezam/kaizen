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
    [Route("/api/orderDetail")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderDetailsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOrderDetailService orderDetailService;
        public OrderDetailsController(IMapper mapper, IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
            this.mapper = mapper;
        }
    }
}