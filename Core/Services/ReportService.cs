using Kaizen.Core.Interfaces;
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

        public async Task<Object> GetTotalSalesByMonthAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository
                .GetTotalSalesByMonthAsync()
                select new
                {
                    Year = o.Year,
                    Month = o.Month,
                    Import = o.TotalImport
                })
                .Where(o => o.Year == DateTime.Now.Year)
                .GroupBy(g => new { g.Month })
                .Select(s => new
                {
                    Month = s.Key.Month,
                    TotalSales = s.Sum(sum => sum.Import)
                }).AsQueryable();
        }

        public async Task<Object> GetTotalSalesByAgentAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository
                .GetTotalSalesByAgentAsync()
                select new
                {
                    o.AgentId,
                    o.AgentName,
                    o.TotalImport
                })
                .GroupBy(g => new { g.AgentId, g.AgentName })
                .Select(s => new
                {
                    AgentName = s.Key.AgentName,
                    TotalSales = s.Sum(sum => sum.TotalImport)
                }).AsQueryable();
        }

        public async Task<Object> GetTopCustomersByMonthAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository
                .GetTopCustomersByMonthAsync()
                where o.OrderDate.Month == DateTime.Now.Month
                select new
                {
                    o.CustomerId,
                    o.CustomerName,
                    o.TotalImport
                })
                .GroupBy(g => new { g.CustomerId, g.CustomerName })
                .Select(s => new
                {
                    Name = s.Key.CustomerName,
                    OrdersNumber = s.Count(),
                    TotalCharged = s.Sum(sum => sum.TotalImport)
                })
                .OrderByDescending(orderBy => orderBy.TotalCharged)
                .Take(5);
        }

        public async Task<Object> GetTopSellingProductsByMonthAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository.GetAllNoTracking()
                join od in await unitOfWork.OrderDetailRepository.GetOrderDetailRptAsync()
                    on o.Id equals od.OrderId
                where o.OrderDate.Month == DateTime.Now.Month
                select new
                {
                    od.ProductId,
                    od.ProductName,
                    od.Quantity,
                    od.TotalImport
                }).GroupBy(g => new { g.ProductId, g.ProductName })
                .Select(s => new
                {
                    Name = s.Key.ProductName,
                    SoldUnits = s.Sum(sum => sum.Quantity),
                    TotalSold = s.Sum(sum => sum.TotalImport)
                }).OrderByDescending(orderBy => orderBy.TotalSold)
                .Take(5);
        }

        public async Task<Object> GetTopAgentAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository
                .GetTopAgentAsync()
                select new
                {
                    o.AgentId,
                    o.AgentName,
                    o.Email,
                    o.TotalImport
                })
                .GroupBy(g => new { g.AgentId, g.AgentName, g.Email })
                .Select(s => new
                {
                    Name = s.Key.AgentName,
                    Email = s.Key.Email,
                    OrdersGenerated = s.Count(),
                    TotalSold = s.Sum(sum => sum.TotalImport)
                }).OrderByDescending(orderBy => orderBy.TotalSold)
                .Take(1).First();
        }
    }
}