using Kaizen.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var cultureInfo = new CultureInfo("en-US");

            return (
                from o in await unitOfWork.OrderRepository.GetAll()
                join od in await unitOfWork.OrderDetailRepository.GetAll()
                on o.Id equals od.OrderId
                join p in await unitOfWork.ProductRepository.GetAll()
                on od.ProductId equals p.Id
                select new
                {
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.ToString("MMMM", cultureInfo),
                    Import = od.UnitPrice * od.Quantity
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
                from c in await unitOfWork.CampaignRepository.GetAll()
                join a in await unitOfWork.UserRepository.GetActiveAgentsAsync()
                on c.UserId equals a.Id
                join cd in await unitOfWork.CampaignDetailRepository.GetAll()
                on c.Id equals cd.CampaignId
                join o in await unitOfWork.OrderRepository.GetAll()
                on cd.Id equals o.CampaignDetailId
                join od in await unitOfWork.OrderDetailRepository.GetAll()
                on o.Id equals od.OrderId
                select new
                {
                    AgentId = a.Id,
                    AgentName = $"{a.FirstName} {a.LastName}",
                    Import = od.UnitPrice * od.Quantity
                })
                .GroupBy(g => new { g.AgentId, g.AgentName })
                .Select(s => new
                {
                    AgentName = s.Key.AgentName,
                    TotalSales = s.Sum(sum => sum.Import)
                }).AsQueryable();
        }

        public async Task<Object> GetTopCustomersAsync()
        {
            var orderDetailsGrouped = (
                from od in await unitOfWork.OrderDetailRepository.GetAll()
                select new
                {
                    OrderId = od.OrderId,
                    Import = od.UnitPrice * od.Quantity
                })
                .GroupBy(g => new { g.OrderId })
                .Select(s => new
                {
                    OrderId = s.Key.OrderId,
                    TotalOrder = s.Sum(sum => sum.Import)
                }).ToList();

            return (
                from o in await unitOfWork.OrderRepository.GetAll()
                join cd in await unitOfWork.CampaignDetailRepository.GetAll()
                on o.CampaignDetailId equals cd.Id
                join c in await unitOfWork.CustomerRepository.GetAll()
                on cd.CustomerId equals c.Id
                join odg in orderDetailsGrouped on o.Id equals odg.OrderId
                where o.OrderDate.Month == DateTime.Now.Month
                select new
                {
                    CustomerId = c.Id,
                    CustomerName = $"{c.FirstName} {c.LastName}",
                    TotalOrder = odg.TotalOrder
                })
                .GroupBy(g => new { g.CustomerId, g.CustomerName })
                .Select(s => new
                {
                    Name = s.Key.CustomerName,
                    OrdersNumber = s.Count(),
                    TotalCharged = s.Sum(sum => sum.TotalOrder)
                }).OrderByDescending(orderBy => orderBy.TotalCharged)
                .ToList().Take(5);
        }

        public async Task<Object> GetTopSellingProductsAsync()
        {
            return (
                from o in await unitOfWork.OrderRepository.GetAll()
                join od in await unitOfWork.OrderDetailRepository.GetAll()
                on o.Id equals od.OrderId
                join p in await unitOfWork.ProductRepository.GetAll()
                on od.ProductId equals p.Id
                where o.OrderDate.Month == DateTime.Now.Month
                select new
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Quantity = od.Quantity,
                    Import = od.UnitPrice * od.Quantity
                }).GroupBy(g => new { g.ProductId, g.Name })
                .Select(s => new
                {
                    Name = s.Key.Name,
                    SoldUnits = s.Sum(sum => sum.Quantity),
                    TotalSold = s.Sum(sum => sum.Import)
                }).OrderByDescending(orderBy => orderBy.TotalSold)
                .ToList().Take(5);
        }

        public async Task<Object> GetTopAgentAsync()
        {
            var orderDetailsGrouped = (
                from od in await unitOfWork.OrderDetailRepository.GetAll()
                select new
                {
                    OrderId = od.OrderId,
                    Import = od.UnitPrice * od.Quantity
                })
                .GroupBy(g => new { g.OrderId })
                .Select(s => new
                {
                    OrderId = s.Key.OrderId,
                    TotalOrder = s.Sum(sum => sum.Import)
                }).ToList();

            return (
                from c in await unitOfWork.CampaignRepository.GetAll()
                join a in await unitOfWork.UserRepository.GetActiveAgentsAsync()
                on c.UserId equals a.Id
                join cd in await unitOfWork.CampaignDetailRepository.GetAll()
                on c.Id equals cd.CampaignId
                join o in await unitOfWork.OrderRepository.GetAll()
                on cd.Id equals o.CampaignDetailId
                join odg in orderDetailsGrouped
                on o.Id equals odg.OrderId
                select new
                {
                    AgentId = a.Id,
                    AgentName = $"{a.FirstName} {a.LastName}",
                    Email = a.Email,
                    TotalOrder = odg.TotalOrder
                })
                .GroupBy(g => new { g.AgentId, g.AgentName, g.Email })
                .Select(s => new
                {
                    Name = s.Key.AgentName,
                    Email = s.Key.Email,
                    OrdersGenerated = s.Count(),
                    TotalSold = s.Sum(sum => sum.TotalOrder)
                }).OrderByDescending(orderBy => orderBy.TotalSold)
                .ToList().Take(1).FirstOrDefault();
        }
    }
}