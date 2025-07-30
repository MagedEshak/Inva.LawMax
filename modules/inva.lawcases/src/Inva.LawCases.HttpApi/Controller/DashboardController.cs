using Inva.LawCases.DTOs.Dashboard;
using Inva.LawCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : LawCasesController, IDashboardAppService
    {
        private readonly IDashboardAppService _dashboardAppService;

        public DashboardController(IDashboardAppService dashboardAppService)
        {
            _dashboardAppService = dashboardAppService;
        }

        [HttpGet("CaseByMonth")]
        [ProducesResponseType(typeof(IEnumerable<CaseByMonthDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CaseByMonthDto>> GetListOfCaseByMonth()
        {
            return await _dashboardAppService.GetListOfCaseByMonth();
        }

        [HttpGet("CaseStatusNumber")]
        [ProducesResponseType(typeof(IEnumerable<CaseStatusNumberDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<CaseStatusNumberDto>> GetListOfCaseStatusNumber()
        {
            return await _dashboardAppService.GetListOfCaseStatusNumber();
        }
    }

}
