using Inva.LawCases.Interfaces;
using Inva.LawMax.DTOs.Lawyer;
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
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : LawCasesController
    {
        public readonly ILawyerAppService _lawyerAppService;

        public LawyerController(ILawyerAppService lawyerAppService)
        {
            _lawyerAppService = lawyerAppService;
        }

        [HttpGet("{id}")]
        public async Task<LawyerDto> GetAsync(Guid id)
        {
            return await _lawyerAppService.GetLawyerByIdAsync(id);
        }


        [HttpGet("all")]
        public async Task<PagedResultDto<LawyerDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return await _lawyerAppService.GetListAsync(input);
        }


        [HttpPost]
        public async Task<LawyerDto> CreateAsync([FromBody] CreateUpdateLawyerDto input)
        {
            return await _lawyerAppService.CreateLawyerAsync(input);
        }

        [HttpPatch("{id}")]
        public async Task<LawyerDto> UpdateAsync(Guid id, [FromBody] CreateUpdateLawyerDto input)
        {
            return await _lawyerAppService.UpdateLawyerAsync(id, input);
        }
        //[HttpPatch("{id}")]
        //public async Task<LawyerDto> UpdateAsync(Guid id, [FromBody] UpdateLawyerDto input)
        //{
        //    return await _lawyerAppService.UpdateLawyerAsync(id, input);
        //}


        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _lawyerAppService.DeleteLawyerAsync(id);
        }
    }
}
