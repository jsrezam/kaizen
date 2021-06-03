using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<SalesByProductReportViewModel>> GetSalesByProductReport()
        {
            var result = new QueryResult<SalesByProductReportViewModel>();

            var orders = await unitOfWork.OrderRepository.GetAll();
            var orderDetails = await unitOfWork.OrderDetailRepository.GetAll();
            var products = await unitOfWork.ProductRepository.GetAll();

            var query = (
                from o in orders
                join od in orderDetails on o.Id equals od.OrderId
                join p in products on od.ProductId equals p.Id
                select new
                {
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month,
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Import = od.UnitPrice * od.Quantity
                })
                .GroupBy(g => new { g.Year, g.Month, g.ProductId, g.ProductName })
                .Select(s => new SalesByProductReportViewModel
                {
                    Year = s.Key.Year,
                    Month = s.Key.Month,
                    ProductId = s.Key.ProductId,
                    ProductName = s.Key.ProductName,
                    TotalSales = s.Sum(sum => sum.Import)
                }).AsQueryable();

            result.TotalItems = query.Count();
            result.Items = query.ToList();

            return result;
        }
    }
}