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
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawCases.Controller
{
    [Route("api/law-cases/lawyer")]
    public class LawyerController : LawCasesController , ILawyerAppService
    {
        public readonly ILawyerAppService _lawyerAppService;

        public LawyerController(ILawyerAppService lawyerAppService)
        {
            _lawyerAppService = lawyerAppService;
        }

        [HttpPost]
        public async Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto lawyerDto)
        {
            return await _lawyerAppService.CreateAsync(lawyerDto);
        }


        [HttpDelete("{lawyerGuid}")]
        public async Task DeleteLawyerAsync(Guid lawyerGuid)
        {
            await _lawyerAppService.DeleteAsync(lawyerGuid);
        }

        [HttpGet("all")]
        public async Task<PagedResultDto<LawyerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _lawyerAppService.GetListAsync(input);
        }

        [HttpGet("{lawyerGuid}")]
        public async Task<LawyerDto> GetLawyerByIdAsync(Guid id)
        {
            return await _lawyerAppService.GetAsync(id);
        }

        [HttpPut]
        public async Task<LawyerDto> UpdateLawyerAsync(Guid id, CreateUpdateLawyerDto lawyerDto)
        {
            return await _lawyerAppService.UpdateAsync(id,lawyerDto);
        }

        Task<LawyerDto> IReadOnlyAppService<LawyerDto, LawyerDto, Guid, PagedAndSortedResultRequestDto>.GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<LawyerDto> ICreateAppService<LawyerDto, CreateUpdateLawyerDto>.CreateAsync(CreateUpdateLawyerDto input)
        {
            throw new NotImplementedException();
        }

        Task<LawyerDto> IUpdateAppService<LawyerDto, Guid, CreateUpdateLawyerDto>.UpdateAsync(Guid id, CreateUpdateLawyerDto input)
        {
            throw new NotImplementedException();
        }

        Task IDeleteAppService<Guid>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
