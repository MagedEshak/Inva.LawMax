using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Case;
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
    public class CaseAppService : BaseApplicationService, ICaseAppService
    {
        private readonly IRepository<Case, Guid> _caseRepo;

        public CaseAppService(IRepository<Case,Guid> caseRepo) 
        {
            _caseRepo = caseRepo;
        }
        public async Task<IEnumerable<CaseDto>> GetAllCaseAsync()
        {
            var cases = await _caseRepo.GetListAsync(); // Assuming _caseRepository is injected
            return ObjectMapper.Map<List<Case>, List<CaseDto>>(cases);
        }

        public async Task<CaseDto> GetCaseByIdAsync(Guid caseGuid)
        {
            var caseEntity = await _caseRepo.WithDetailsAsync(l => l.Id);

            var entity = caseEntity.FirstOrDefault(l => l.Id == caseGuid);

            if (entity == null)
            {
                throw new EntityNotFoundException("error");
            }

            return ObjectMapper.Map<Case, CaseDto>(entity);
        }


        public async Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto)
        {
            var caseEntity = ObjectMapper.Map<CreateUpdateCaseDto, Case>(caseDto);

            var insertedCase = await _caseRepo.InsertAsync(caseEntity, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(insertedCase);
        }

        public async Task<CaseDto> UpdateCaseAsync(Guid id,CreateUpdateCaseDto caseDto)
        {
            var caseEntityId = await _caseRepo.GetAsync(id);

            if (caseEntityId == null)
            {
                throw new EntityNotFoundException("This Lawyer Not Found");
            }

            ObjectMapper.Map(caseDto, caseEntityId);

            await _caseRepo.UpdateAsync(caseEntityId, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(caseEntityId);
        }

        public async Task<bool> DeleteCaseAsync(Guid caseGuid)
        {
            var caseEntityId = await _caseRepo.GetAsync(caseGuid);

            if (caseEntityId == null)
            {
                return false;
            }

            await _caseRepo.DeleteAsync(caseGuid);

            return true;
        }

       
    }
}
