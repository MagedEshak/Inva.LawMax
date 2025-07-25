using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases.Controller
{
    [Route("api/law-cases/hearing")]
    public class HearingController : LawCasesController, IHearingAppService
    {
        public readonly IHearingAppService _hearingAppService;

        public HearingController(IHearingAppService hearingAppService)
        {
            _hearingAppService = hearingAppService;
        }
        [HttpPost]
        public async Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearingDto)
        {
            return await _hearingAppService.CreateHearingAsync(hearingDto);
        }
        [HttpDelete]
        public async Task<bool> DeleteHearingAsync(Guid hearingGuid)
        {
            return await _hearingAppService.DeleteHearingAsync(hearingGuid);
        }
        [HttpGet]
        public async Task<HearingDto> GetHearingByIdAsync(Guid hearingGuid)
        {
            return await _hearingAppService.GetHearingByIdAsync(hearingGuid);
        }
        [HttpPut]
        public async Task<HearingDto> UpdateHearingAsync(CreateUpdateHearingDto hearingDto)
        {
            return await _hearingAppService.UpdateHearingAsync(hearingDto);
        }
    }
}
