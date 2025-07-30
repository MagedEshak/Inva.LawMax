using Inva.LawCases.Base;
using Inva.LawCases.CaseRepo;
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.DTOs.Lawyer;
using Inva.LawCases.HearingRepo;
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
        private readonly ICaseRepository _caseRepo;
        private readonly IHearingRepository _hearingRepo;

        public CaseAppService(ICaseRepository caseRepo, IHearingRepository hearingRepo)
        {
            _caseRepo = caseRepo;
            _hearingRepo = hearingRepo;
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
            var caseEntity = await _caseRepo.GetQueryableAsync();

            var entity = await caseEntity
                .Where(c => c.Id == caseGuid)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException("Case not found");
            }

            var dto = ObjectMapper.Map<Case, CaseDto>(entity);

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
                throw new EntityNotFoundException("This Case Not Found");
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

        public async Task<PagedResultDto<CaseLawyerHearingsWithNavigationProperty>> GetCaseWithLawyersAndHearingsListAsync(PagedAndSortedResultRequestDto input)
        {

            var query = await _caseRepo.GetQueryableAsync();
            query = query.Include(c => c.Lawyer);
            query = query.Include(c => c.Hearing);
            
            //if (date != null)
            //{
            //    query.Where(x => x.Hearing.Date == date);

            //}

            query = query.OrderBy(input.Sorting ?? "Title");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

        
            var items = await query.Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();


            var result = items.Select(cases => new CaseLawyerHearingsWithNavigationProperty
            {
                CaseDto = ObjectMapper.Map<Case, CaseDto>(cases),
                LawyerDto = cases.Lawyer != null ? ObjectMapper.Map<Lawyer, LawyerDto>(cases.Lawyer) : null,
                HearingDto = cases.Hearing !=null ? ObjectMapper.Map<Hearing, HearingDto>(cases.Hearing) : null,

            }).ToList();

            return new PagedResultDto<CaseLawyerHearingsWithNavigationProperty>(totalCount, result);
        }

        //public async Task<PagedResultDto<CaseHearingWithNavigationProperty>> GetCaseWithHearingListAsync(PagedAndSortedResultRequestDto input)
        //{

        //    var query = await _caseRepo.GetQueryableAsync();
        //    query = query.Include(c => c.Hearing);

        //    query = query.OrderBy(input.Sorting ?? "Title");

        //    // إجمالي العناصر قبل التصفية
        //    var totalCount = await AsyncExecuter.CountAsync(query);


        //    var items = await query.Skip(input.SkipCount)
        //               .Take(input.MaxResultCount)
        //               .ToListAsync();


        //    var result = items.Select(cases => new CaseHearingWithNavigationProperty
        //    {
        //        CaseDto = ObjectMapper.Map<Case, CaseDto>(cases),
        //        HearingDto = cases.Hearing != null ? ObjectMapper.Map<Hearing, HearingDto>(cases.Hearing) : null
        //    }).ToList();

        //    return new PagedResultDto<CaseHearingWithNavigationProperty>(totalCount, result);
        //}

        public async Task<CaseLawyerHearingsWithNavigationProperty> GetCaseWithLawyersAndHearingsByIdAsync(Guid caseGuid)
        {
            var cases = await _caseRepo.GetCaseWithLawyer(caseGuid);
            var hearing = await _hearingRepo.GetHearingWithCaseID(caseGuid);

            if (cases == null && hearing == null)
            {
                throw new EntityNotFoundException("Case Not Found");
            }
            return new CaseLawyerHearingsWithNavigationProperty
            {
                CaseDto = ObjectMapper.Map<Case,CaseDto>(cases),
                LawyerDto = ObjectMapper.Map<Lawyer, LawyerDto>(cases.Lawyer),
                HearingDto = ObjectMapper.Map<Hearing, HearingDto>(hearing)
            };
        }

        //public async Task<CaseHearingWithNavigationProperty> GetCaseHearingByIdAsync(Guid caseGuid)
        //{
        //    var cases = await _caseRepo.GetCaseWithLawyer(caseGuid);


        //    if (cases == null)
        //    {
        //        throw new EntityNotFoundException("Hearing Not Found");
        //    }
        //    return new CaseHearingWithNavigationProperty
        //    {
        //        CaseDto = ObjectMapper.Map<Case, CaseDto>(cases),
        //        HearingDto = ObjectMapper.Map<Hearing, HearingDto>(cases.Hearing)
        //    };
        //}

       
    }
}
