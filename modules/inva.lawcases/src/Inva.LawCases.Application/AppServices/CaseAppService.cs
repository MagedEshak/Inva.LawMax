using Inva.LawCases.Base;
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Interfaces;
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

namespace Inva.LawCases.AppServices
{
    public class CaseAppService : BaseApplicationService, ICaseAppService
    {
        private readonly IRepository<Case, Guid> _caseRepo;

        public CaseAppService(IRepository<Case, Guid> caseRepo)
        {
            _caseRepo = caseRepo;
        }

        public async Task<PagedResultDto<CaseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _caseRepo.GetQueryableAsync();

            // تطبيق الترتيب (لو فيه)
            query = query.OrderBy(input.Sorting ?? "Title");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Pagination
            var items = await AsyncExecuter.ToListAsync(
                query.Skip(input.SkipCount).Take(input.MaxResultCount)
            );

            // تحويل للـ DTO
            var caseDtos = ObjectMapper.Map<List<Case>, List<CaseDto>>(items);

            return new PagedResultDto<CaseDto>(totalCount, caseDtos);
        }


        public async Task<CaseDto> GetCaseByIdAsync(Guid caseGuid)
        {
            var caseEntity = await _caseRepo
                .WithDetailsAsync(c => c.Lawyer, c => c.Hearing); // Include Lawyer & Hearing

            var entity = await caseEntity
                .Where(c => c.Id == caseGuid)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException("Case not found");
            }

            var dto = ObjectMapper.Map<Case, CaseDto>(entity);

            // لو ما استخدمتش AutoMapper للمعلومات الملاحقة:
            dto.LawyerName = entity.Lawyer?.Name;
            dto.LawyerEmail = entity.Lawyer?.Email;
            dto.LawyerPhone = entity.Lawyer?.Phone;
            dto.LawyerSpeciality = entity.Lawyer?.Speciality;

            dto.HearingDate = entity.Hearing?.Date ?? DateTime.MinValue;
            dto.HearingLocation = entity.Hearing?.Location;

            return dto;
        }



        public async Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto)
        {
            var caseEntity = ObjectMapper.Map<CreateUpdateCaseDto, Case>(caseDto);

            var insertedCase = await _caseRepo.InsertAsync(caseEntity, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(insertedCase);
        }

        public async Task<CaseDto> UpdateCaseAsync(Guid id, CreateUpdateCaseDto caseDto)
        {
            var cases = await _caseRepo.GetAsync(id);

            if (cases == null)
            {
                throw new EntityNotFoundException("This Lawyer Not Found");
            }

            if (string.IsNullOrWhiteSpace(caseDto.ConcurrencyStamp) || caseDto.ConcurrencyStamp != cases.ConcurrencyStamp)
            {
                throw new AbpDbConcurrencyException("The record has been modified by someone else.");
            }

            if (caseDto.Title != null)
                cases.Title = caseDto.Title;

            if (caseDto.Description != null)
                cases.Description = caseDto.Description;

            if (caseDto.Status != null)
                cases.Status = (Enums.Status)caseDto.Status;

            if (caseDto.LawyerId != null)
                cases.LawyerId = caseDto.LawyerId;

            if (caseDto.HearingId != null)
                cases.HearingId = caseDto.HearingId;


            await _caseRepo.UpdateAsync(cases, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(cases);
        }

        public async Task<bool> DeleteCaseAsync(Guid caseGuid)
        {
            var cases = await _caseRepo.GetAsync(caseGuid);

            if (cases == null)
            {
                return false;
            }

            await _caseRepo.DeleteAsync(caseGuid, autoSave: true);

            return true;
        }


    }
}
