using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Inva.LawMax.DTOs.Lawyer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HearingController : LawCasesController, IHearingAppService
    {
        public readonly IHearingAppService _hearingAppService;

        public HearingController(IHearingAppService hearingAppService)
        {
            _hearingAppService = hearingAppService;
        }

        [HttpGet("all")]
        public async Task<PagedResultDto<HearingWithNavigationPropertyDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return await _hearingAppService.GetListAsync(input);
        }


        [HttpGet("{hearingGuid}")]
        public async Task<HearingWithNavigationPropertyDto> GetHearingByIdAsync(Guid hearingGuid)
        {
            return await _hearingAppService.GetHearingByIdAsync(hearingGuid);
        }

        [HttpPost]
        public async Task<HearingDto> CreateHearingAsync([FromBody] CreateUpdateHearingDto hearingDto)
        {
            return await _hearingAppService.CreateHearingAsync(hearingDto);
        }
       
        [HttpPatch("{id}")]
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
