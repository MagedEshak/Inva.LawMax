using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.DTOs.Lawyer;
using Inva.LawCases.Interfaces;
using Inva.LawCases.IRepositories;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Inva.LawCases.AppServices
{
    public class HearingAppService : BaseApplicationService, IHearingAppService
    {
        private readonly IHearingRepository _hearingRepo;

        public HearingAppService(IHearingRepository hearingRepo)
        {
            _hearingRepo = hearingRepo;
        }

        public async Task<PagedResultDto<HearingWithNavigationPropertyDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _hearingRepo.GetQueryableAsync();
            query = query.Include(x => x.Case).ThenInclude(h => h.Lawyer);

            // تطبيق الترتيب (لو فيه)
            query = query.OrderBy(input.Sorting ?? "Location");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Pagination
            var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

            var result = items.Select(Hearings => new HearingWithNavigationPropertyDto
            {
                Hearing = ObjectMapper.Map<Hearing, HearingDto>(Hearings),
                Case = Hearings.Case != null ? ObjectMapper.Map<Case, CaseDto>(Hearings.Case) : null
            }).ToList();

            return new PagedResultDto<HearingWithNavigationPropertyDto>(totalCount, result);
        }



        public async Task<HearingWithNavigationPropertyDto> GetHearingByIdAsync(Guid id)
        {
            var hearing = await _hearingRepo.GetHearingWithCase(id);

            if (hearing == null)
            {
                throw new EntityNotFoundException("Hearing Not Found");
            }

            return new HearingWithNavigationPropertyDto
            {
                Hearing = ObjectMapper.Map<Hearing, HearingDto>(hearing),
                Case = ObjectMapper.Map<Case, CaseDto>(hearing.Case)
            };
        }
        public async Task<List<HearingDto>> GetHearingsByLawyerAsync(Guid lawyerId)
        {
            var hearing = await _hearingRepo.GetHearingsByLawyer(lawyerId);

            if (hearing == null)
            {
                throw new EntityNotFoundException("Hearing Not Found");
            }
            return hearing;
        }

       

        public async Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearing)
        {
            var hearingEntity = ObjectMapper.Map<CreateUpdateHearingDto, Hearing>(hearing);

            var insertedHearing = await _hearingRepo.InsertAsync(hearingEntity, autoSave: true);

            return ObjectMapper.Map<Hearing, HearingDto>(insertedHearing);
        }

        public async Task<HearingDto> UpdateHearingAsync(Guid id, CreateUpdateHearingDto hearingDto)
        {
            var hearing = await _hearingRepo.GetAsync(id);

            if (hearing == null)
            {
                throw new EntityNotFoundException("This Hearing Not Found");
            }

            if (string.IsNullOrWhiteSpace(hearingDto.ConcurrencyStamp) || hearingDto.ConcurrencyStamp != hearing.ConcurrencyStamp)
            {
                throw new AbpDbConcurrencyException("The record has been modified by someone else.");
            }

            if (hearingDto.Date != null)
                hearing.Date = (DateTime)hearingDto.Date;

            if (hearingDto.Location != null)
                hearing.Location = hearingDto.Location;

            if (hearingDto.Decision != null)
                hearing.Decision = hearingDto.Decision;


            await _hearingRepo.UpdateAsync(hearing, autoSave: true);

            return ObjectMapper.Map<Hearing, HearingDto>(hearing);
        }

        public async Task<bool> DeleteHearingAsync(Guid hearing)
        {
            var hearingEntity = await _hearingRepo.GetAsync(hearing);

            if (hearingEntity == null)
            {
                return false;
            }

            await _hearingRepo.DeleteAsync(hearing, autoSave: true);
            return true;
        }


    }
}
