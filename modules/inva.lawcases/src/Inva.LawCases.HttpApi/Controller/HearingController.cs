using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/law-cases/hearing")]
    public class HearingController : LawCasesController, IHearingAppService
    {
        public readonly IHearingAppService _hearingAppService;

        public HearingController(IHearingAppService hearingAppService)
        {
            _hearingAppService = hearingAppService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<HearingDto>> GetAllHearingAsync()
        {
            return await _hearingAppService.GetAllHearingAsync();
        }

        [HttpGet("{hearingGuid}")]
        public async Task<HearingDto> GetHearingByIdAsync(Guid hearingGuid)
        {
            return await _hearingAppService.GetHearingByIdAsync(hearingGuid);
        }

        [HttpPost]
        public async Task<HearingDto> CreateHearingAsync([FromBody] CreateUpdateHearingDto hearingDto)
        {
            return await _hearingAppService.CreateHearingAsync(hearingDto);
        }
       
        [HttpPut("{id}")]
        public async Task<HearingDto> UpdateHearingAsync(Guid id, [FromBody] CreateUpdateHearingDto hearingDto)
        {
            return await _hearingAppService.UpdateHearingAsync(id, hearingDto);
        }

        [HttpDelete("{hearingGuid}")]
        public async Task<bool> DeleteHearingAsync(Guid hearingGuid)
        {
            return await _hearingAppService.DeleteHearingAsync(hearingGuid);
        }
      
    }
}
