using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IReportService
    {
        Task<QueryResult<SalesByProductReportViewModel>> GetSalesByProductReport();
    }
}