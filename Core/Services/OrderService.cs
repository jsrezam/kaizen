using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICampaignDetailService campaignDetailService;
        private readonly IProductService productService;

        public OrderService(IUnitOfWork unitOfWork, ICampaignDetailService campaignDetailService, IProductService productService)
        {
            this.productService = productService;
            this.campaignDetailService = campaignDetailService;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await unitOfWork.OrderRepository.AddAsync(order);
            await UpdateCampaignDetailOrderAsync(order.CampaignDetailId);
            await UpdateOrderProductsStockAsync(order.OrderDetails);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateCampaignDetailOrderAsync(int campaignDetailOrderId)
        {
            var campaignDetail = await campaignDetailService.GetCampaignDetailItemAsync(campaignDetailOrderId);
            campaignDetail.State = CampaignStatus.Earned.ToString();
            await campaignDetailService.UpdateCampaignDetailItem(campaignDetail);
        }

        private async Task UpdateOrderProductsStockAsync(IEnumerable<OrderDetail> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                var product = await productService.GetProductAsync(orderDetail.ProductId);
                product.UnitsInStock -= orderDetail.Quantity;
                product.UnitsOnOrder += orderDetail.Quantity;
                await productService.UpdateProductAsync(product);
            }
        }

        public async Task<QueryResult<Order>> GetOrdersAsync(OrderQuery queryObj)
        {
            return await unitOfWork.OrderRepository.GetOrdersAsync(queryObj);
        }

        public async Task<QueryResult<Order>> GetAgentOrdersAsync(string userId, OrderQuery queryObj)
        {
            return await unitOfWork.OrderRepository.GetAgentOrdersAsync(userId, queryObj);
        }

        public async Task<OrderViewModel> GetOrderDetailAsync(int orderId)
        {
            var orderDetail = await unitOfWork.OrderDetailRepository.GetOrderDetailAsync(orderId, new OrderDetailQuery());
            var products = await unitOfWork.ProductRepository.GetAll();
            var order = (await unitOfWork.OrderRepository.GetOrderAsync(orderId));
            var orderOwner = order.CampaignDetail.Customer;

            return new OrderViewModel
            {
                Customer = orderOwner,
                CampaignId = order.CampaignDetail.CampaignId,
                CampaignDetailId = order.CampaignDetailId,
                OrderDetailViewModel = (from p in products
                                        join od in orderDetail.Items on p.Id equals od.ProductId
                                        select new ItemOrderDetailViewModel
                                        {
                                            Product = od.Product,
                                            UnitPrice = od.UnitPrice,
                                            Quantity = od.Quantity,
                                            Import = (od.UnitPrice * od.Quantity),
                                            Discount = od.Discount,
                                        }).ToList(),
            };
        }
    }
}