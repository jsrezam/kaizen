using System;
using System.Threading.Tasks;

namespace Kaizen.Core.Interfaces
{
    public interface IReportService
    {
        Task<Object> GetTotalSalesByMonthAsync();
        Task<Object> GetTotalSalesByAgentAsync();
        Task<Object> GetTopCustomersByMonthAsync();
        Task<Object> GetTopSellingProductsByMonthAsync();
        Task<Object> GetTopAgentAsync();
    }
}