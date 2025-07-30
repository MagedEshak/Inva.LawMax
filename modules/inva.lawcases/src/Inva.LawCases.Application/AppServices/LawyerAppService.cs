
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.DTOs.Lawyer;
using Inva.LawCases.Interfaces;
using Inva.LawCases.LawyerRepo;
using Inva.LawCases.LawyerRepo.IlawyerReepository;
using Inva.LawCases.Models;
using Inva.LawMax.DTOs.Lawyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;


namespace Inva.LawCases.AppServices
{
    public class LawyerAppService : ApplicationService, ILawyerAppService
    {
        private readonly ILawyerRepository _lawyerRepo;

        public LawyerAppService(ILawyerRepository lawyerRepo)
        {
            _lawyerRepo = lawyerRepo;
        }

        public async Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto lawyerDto)
        {
            try
            {
                var lawyerEntity = ObjectMapper.Map<CreateUpdateLawyerDto, Models.Lawyer>(lawyerDto);

                var insertedLawyer = await _lawyerRepo.InsertAsync(lawyerEntity, autoSave: true);

                return ObjectMapper.Map<Lawyer, LawyerDto>(insertedLawyer);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in CreateLawyerAsync");
                throw;
            }
        }

        public async Task<bool> DeleteLawyerAsync(Guid lawyerGuid)
        {
            var lawyer = await _lawyerRepo.FindAsync(lawyerGuid);

            if (lawyer == null)
            {
                return false;
            }

            await _lawyerRepo.DeleteAsync(lawyer, autoSave: true);
            return true;
        }


        public async Task<PagedResultDto<LawyerWithNavigationPropertyDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _lawyerRepo.GetQueryableAsync();
            query = query.Include(c => c.Case).ThenInclude(h=>h.Hearing);

            query = query.OrderBy(input.Sorting ?? "Name");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Pagination
            //var items = await _lawyerRepo.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, true);

            var items = await query.Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();


            var result = items.Select(lawyer => new LawyerWithNavigationPropertyDto
            {
                Lawyer = ObjectMapper.Map<Lawyer, LawyerDto>(lawyer),
                Case = lawyer.Case != null ? ObjectMapper.Map<Case, CaseDto>(lawyer.Case) : null
            }).ToList();

            return new PagedResultDto<LawyerWithNavigationPropertyDto>(totalCount, result);
        }


        public async Task<LawyerWithNavigationPropertyDto> GetLawyerByIdAsync(Guid ID)
        {
            var lawyer = await _lawyerRepo.GetLawyerWithCase(ID);


            if (lawyer == null)
            {
                throw new EntityNotFoundException("Lawyer Not Found");
            }
            return new LawyerWithNavigationPropertyDto
            {
                Lawyer = ObjectMapper.Map<Lawyer, LawyerDto>(lawyer),
                Case = ObjectMapper.Map<Case, CaseDto>(lawyer.Case)
            };
        }

        public async Task<LawyerDto> UpdateLawyerAsync(Guid id, CreateUpdateLawyerDto lawyerDto)
        {
            var lawyer = await _lawyerRepo.GetAsync(id);

            if (lawyer == null)
            {
                throw new EntityNotFoundException("This Lawyer Not Found");
            }

            // تحقق من الـ ConcurrencyStamp
            if (string.IsNullOrWhiteSpace(lawyerDto.ConcurrencyStamp) || lawyerDto.ConcurrencyStamp != lawyer.ConcurrencyStamp)
            {
                throw new AbpDbConcurrencyException("The record has been modified by someone else.");
            }

            if (lawyerDto.Name != null)
                lawyer.Name = lawyerDto.Name;

            if (lawyerDto.Email != null)
                lawyer.Email = lawyerDto.Email;

            if (lawyerDto.Phone != null)
                lawyer.Phone = lawyerDto.Phone;

            if (lawyerDto.Address != null)
                lawyer.Address = lawyerDto.Address;

            if (lawyerDto.Speciality != null)
                lawyer.Speciality = lawyerDto.Speciality;

            if (lawyerDto.CaseId != null)
                lawyer.CaseId = lawyerDto.CaseId;


            await _lawyerRepo.UpdateAsync(lawyer, autoSave: true);

            return ObjectMapper.Map<Lawyer, LawyerDto>(lawyer);
        }

    }
}
