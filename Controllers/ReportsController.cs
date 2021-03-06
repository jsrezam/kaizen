using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Common;
using Kaizen.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("api/reports")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.AdminRoleValue)]
    public class ReportsController : Controller
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet("totalSalesByMonth")]
        public async Task<IActionResult> GetTotalSalesByMonthAsync()
        {
            var report = await reportService.GetTotalSalesByMonthAsync();
            return Ok(report);
        }

        [HttpGet("totalSalesByAgent")]
        public async Task<IActionResult> GetTotalSalesByAgentAsync()
        {
            var report = await reportService.GetTotalSalesByAgentAsync();
            return Ok(report);
        }

        [HttpGet("topCustomers")]
        public async Task<IActionResult> GetTopCustomersByMonthAsync()
        {
            var report = await reportService.GetTopCustomersByMonthAsync();
            return Ok(report);
        }

        [HttpGet("topSellingProducts")]
        public async Task<IActionResult> GetTopSellingProductsAsync()
        {
            var report = await reportService.GetTopSellingProductsByMonthAsync();
            return Ok(report);
        }

        [HttpGet("topAgent")]
        public async Task<IActionResult> GetTopAgentAsync()
        {
            var report = await reportService.GetTopAgentAsync();
            return Ok(report);
        }

    }
}