using Inva.LawCases.DTOs.Case;
using Inva.LawCases.Interfaces;
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
  
    [Route("api/law-cases/case")]
    public class CaseController : AbpController, ICaseAppService
    {
        public readonly ICaseAppService _caseAppService;

        public CaseController(ICaseAppService caseAppService)
        {
            _caseAppService = caseAppService;
        }
        [HttpPost]
        public async Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto)
        {
           return await _caseAppService.CreateCaseAsync(caseDto);
        }
        [HttpDelete]
        public async Task<bool> DeleteCaseAsync(Guid caseGuid)
        {
            return await _caseAppService.DeleteCaseAsync(caseGuid);
        }
        [HttpGet]
        public async Task<CaseDto> GetCaseByIdAsync(Guid caseGuid)
        {
            return await _caseAppService.GetCaseByIdAsync(caseGuid);
        }
        [HttpPut]
        public async Task<CaseDto> UpdateCaseAsync(CreateUpdateCaseDto caseDto)
        {
            return await _caseAppService.UpdateCaseAsync(caseDto);
        }
    }
}
