using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace Inva.LawCases.AppServices
{
    public class HearingAppService : BaseApplicationService, IHearingAppService
    {
        private readonly IRepository<Hearing, Guid> _hearingRepo;

        public HearingAppService(IRepository<Hearing, Guid> hearingRepo)
        {
            _hearingRepo = hearingRepo;
        }

        public async Task<PagedResultDto<HearingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _hearingRepo.GetQueryableAsync();

            // تطبيق الترتيب (لو فيه)
            query = query.OrderBy(input.Sorting ?? "Location");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Pagination
            var items = await AsyncExecuter.ToListAsync(
                query.Skip(input.SkipCount).Take(input.MaxResultCount)
            );

            // تحويل للـ DTO
            var hearingDtos = ObjectMapper.Map<List<Hearing>, List<HearingDto>>(items);

            return new PagedResultDto<HearingDto>(totalCount, hearingDtos);
        }



        public async Task<HearingDto> GetHearingByIdAsync(Guid hearing)
        {
            var hearingEntity = await _hearingRepo.WithDetailsAsync(h => h.Id);
            var entity = hearingEntity.FirstOrDefault(h => h.Id == hearing);

            if (entity == null)
            {

                throw new EntityNotFoundException("Hearing Not Found");
            }

            return ObjectMapper.Map<Hearing, HearingDto>(entity);
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

           
            if (hearingDto.Date != null)
                hearing.Date = (DateTime)hearingDto.Date;

            if (hearingDto.Location != null)
                hearing.Location = hearingDto.Location;


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
