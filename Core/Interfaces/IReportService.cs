using System;
using System.Threading.Tasks;

namespace Kaizen.Core.Interfaces
{
    public interface IReportService
    {
        Task<Object> GetTotalSalesByMonthAsync();
        Task<Object> GetTotalSalesByAgentAsync();
        Task<Object> GetTopCustomersAsync();
        Task<Object> GetTopSellingProductsAsync();
        Task<Object> GetTopAgentAsync();
    }
}