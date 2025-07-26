using Inva.LawCases.DTOs.Case;
using Inva.LawCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/law-cases/case")]
    public class CaseController : LawCasesController, ICaseAppService
    {
        public readonly ICaseAppService _caseAppService;

        public CaseController(ICaseAppService caseAppService)
        {
            _caseAppService = caseAppService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<CaseDto>> GetAllCaseAsync()
        {
           return await _caseAppService.GetAllCaseAsync();
        }

        [HttpGet("{caseGuid}")]
        public async Task<CaseDto> GetCaseByIdAsync(Guid caseGuid)
        {
            return await _caseAppService.GetCaseByIdAsync(caseGuid);
        }

        [HttpPost]
        public async Task<CaseDto> CreateCaseAsync([FromBody] CreateUpdateCaseDto caseDto)
        {
           return await _caseAppService.CreateCaseAsync(caseDto);
        }

        [HttpPut("{id}")]
        public async Task<CaseDto> UpdateCaseAsync(Guid id, [FromBody] CreateUpdateCaseDto caseDto)
        {
            return await _caseAppService.UpdateCaseAsync(id, caseDto);
        }

        [HttpDelete("{caseGuid}")]
        public async Task<bool> DeleteCaseAsync(Guid caseGuid)
        {
            return await _caseAppService.DeleteCaseAsync(caseGuid);
        }

       

        
    }
}
