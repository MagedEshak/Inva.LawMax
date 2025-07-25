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
    [Route("api/law-cases/lawyer")]
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
            return await _lawyerAppService.GetAsync(id);
        }

        [HttpGet("all")]
        public async Task<PagedResultDto<LawyerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _lawyerAppService.GetListAsync(input);
        }
        [HttpPost]
        public async Task<LawyerDto> CreateAsync(CreateUpdateLawyerDto input)
        {
            return await _lawyerAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<LawyerDto> UpdateAsync(Guid id, CreateUpdateLawyerDto input)
        {
            return await _lawyerAppService.UpdateAsync(id, input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _lawyerAppService.DeleteAsync(id);
        }
    }
}
