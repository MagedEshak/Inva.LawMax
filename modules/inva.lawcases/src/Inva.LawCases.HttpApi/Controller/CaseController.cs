﻿using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : LawCasesController, ICaseAppService
    {
        public readonly ICaseAppService _caseAppService;

        public CaseController(ICaseAppService caseAppService)
        {
            _caseAppService = caseAppService;
        }

        [HttpGet("all")]
        public async Task<PagedResultDto<CaseDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return await _caseAppService.GetListAsync(input);
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

        [HttpPatch("{id}")]
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
