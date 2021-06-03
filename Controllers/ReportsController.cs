using System;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Utilities;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
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
        private readonly IMapper mapper;
        public ReportsController(IMapper mapper, IReportService reportService)
        {
            this.mapper = mapper;
            this.reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesByProductReport()
        {
            var report = await reportService.GetSalesByProductReport();
            var reportDto = mapper.Map<QueryResult<SalesByProductReportViewModel>, QueryResultDto<SalesByProductReportViewModelDto>>(report);
            return Ok(reportDto);
        }

    }
}