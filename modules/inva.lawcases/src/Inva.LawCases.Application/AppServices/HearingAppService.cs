using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Inva.LawCases.AppServices
{
    public class HearingAppService : BaseApplicationService, IHearingAppService
    {
        private readonly IRepository<Hearing, Guid> _hearingRepo;

        public HearingAppService(IRepository<Hearing, Guid> hearingRepo)
        {
            _hearingRepo = hearingRepo;
        }
        public async Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearing)
        {
            var hearingEntity = ObjectMapper.Map<CreateUpdateHearingDto, Hearing>(hearing);

            var insertedHearing = await _hearingRepo.InsertAsync(hearingEntity, autoSave: true);

            return ObjectMapper.Map<Hearing, HearingDto>(insertedHearing);
        }

        public async Task<bool> DeleteHearingAsync(Guid hearing)
        {
            var hearingEntity = await _hearingRepo.GetAsync(hearing);

            if (hearingEntity == null)
            {
                return false;
            }

            await _hearingRepo.DeleteAsync(hearing);
            return true;
        }

        public async Task<HearingDto> GetHearingByIdAsync(Guid hearing)
        {
            var hearingEntity = await _hearingRepo.WithDetailsAsync(h => h.Id);
            var entity = hearingEntity.FirstOrDefault(h => h.Id == hearing);

            if (entity == null)
            {

                throw new EntityNotFoundException("error");
            }

            return ObjectMapper.Map<Hearing, HearingDto>(entity);
        }

        public async Task<HearingDto> UpdateHearingAsync(CreateUpdateHearingDto hearing)
        {
            var hearingEntity = await _hearingRepo.GetAsync(hearing.Id);

            if (hearingEntity == null)
            {
                throw new EntityNotFoundException("This Hearing Not Found");
            }

            ObjectMapper.Map(hearingEntity, hearing);
            await _hearingRepo.UpdateAsync(hearingEntity, autoSave: true);

            return ObjectMapper.Map<Hearing, HearingDto>(hearingEntity);
        }
    }
}
