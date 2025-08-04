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

namespace Inva.LawCases.AppServices
{
    /// <summary>
    /// Provides application-level services for managing cases, including creating, updating, retrieving,  and deleting
    /// case records, as well as retrieving related data such as lawyers and hearings.
    /// </summary>
    /// <remarks>This service acts as a bridge between the domain layer and the application layer, exposing 
    /// functionality for managing cases and their associated entities. It supports operations such as  paginated
    /// retrieval, detailed lookups, and CRUD operations for cases. Additionally, it provides  methods to retrieve cases
    /// along with their related lawyers and hearings.</remarks>
    public class CaseAppService : BaseApplicationService, ICaseAppService
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IHearingRepository _hearingRepo;

        public CaseAppService(ICaseRepository caseRepo, IHearingRepository hearingRepo)
        {
            _caseRepo = caseRepo;
            _hearingRepo = hearingRepo;
        }
        /// <summary>
        /// Retrieves a paginated and sorted list of cases, optionally filtered by a specific date.
        /// </summary>
        /// <remarks>The method supports sorting by the specified property in <paramref name="input"/>. If
        /// no sorting is provided, the default sorting is by "CaseTitle". The result is paginated based on the skip and
        /// take values in <paramref name="input"/>.</remarks>
        /// <param name="input">The pagination and sorting parameters, including the number of items to skip and take, and the sorting
        /// criteria.</param>
        /// <param name="date">An optional date to filter the cases. If provided, only cases created on the specified date are included in
        /// the result.</param>
        /// <returns>A <see cref="PagedResultDto{T}"/> containing the total count of cases and a list of <see cref="CaseDto"/>
        /// objects that match the specified criteria.</returns>
        public async Task<PagedResultDto<CaseDto>> GetListAsync(PagedAndSortedResultRequestDto input, DateTime? date)
        {
            var query = await _caseRepo.GetQueryableAsync();

            query = query
                        .Include(x => x.Lawyer)
                        .Include(x => x.Hearings);
            if (date != null)
            {
                var targetDate = date.Value.Date;
                query = query.Where(d => d.CreationTime.Date == targetDate);
            }

            query = query.OrderBy(input.Sorting ?? "CaseTitle");


            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(
                query.Skip(input.SkipCount).Take(input.MaxResultCount)
            );

            var caseDtos = ObjectMapper.Map<List<Case>, List<CaseDto>>(items);

            return new PagedResultDto<CaseDto>(totalCount, caseDtos);
        }
        /// <summary>
        /// Retrieves a case by its unique identifier.
        /// </summary>
        /// <remarks>This method queries the underlying data source to find a case matching the provided identifier. If no
        /// matching case is found, an exception is thrown.</remarks>
        /// <param name="caseGuid">The unique identifier of the case to retrieve.</param>
        /// <returns>A <see cref="CaseDto"/> representing the case with the specified identifier.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if no case with the specified <paramref name="caseGuid"/> is found.</exception>
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
        /// <summary>
        /// Creates a new case and returns the created case details.
        /// </summary>
        /// <remarks>This method maps the provided <paramref name="caseDto"/> to a case entity, inserts it
        /// into the repository,  and returns the created case as a data transfer object.</remarks>
        /// <param name="caseDto">The data transfer object containing the details of the case to create.</param>
        /// <returns>A <see cref="CaseDto"/> representing the newly created case.</returns>
        public async Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto)
        {
            var caseEntity = ObjectMapper.Map<CreateUpdateCaseDto, Case>(caseDto);

            var insertedCase = await _caseRepo.InsertAsync(caseEntity, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(insertedCase);
        }
        /// <summary>
        /// Updates an existing case with the specified details.
        /// </summary>
        /// <remarks>This method updates the properties of an existing case based on the provided
        /// <paramref name="caseDto"/>.  Only non-null properties in <paramref name="caseDto"/> will be updated.  The
        /// method ensures that the case has not been modified by another process by validating the concurrency
        /// stamp.</remarks>
        /// <param name="id">The unique identifier of the case to update.</param>
        /// <param name="caseDto">An object containing the updated details of the case.  The <see
        /// cref="CreateUpdateCaseDto.ConcurrencyStamp"/> must match the current concurrency stamp of the case.</param>
        /// <returns>A <see cref="CaseDto"/> object representing the updated case.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if a case with the specified <paramref name="id"/> does not exist.</exception>
        /// <exception cref="AbpDbConcurrencyException">Thrown if the <see cref="CreateUpdateCaseDto.ConcurrencyStamp"/> does not match the current concurrency
        /// stamp of the case.</exception>
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


            if (caseDto.CaseTitle != null)
                cases.CaseTitle = caseDto.CaseTitle;

            if (caseDto.Description != null)
                cases.Description = caseDto.Description;

            if (caseDto.FinalVerdict != null)
                cases.FinalVerdict = caseDto.FinalVerdict;

            if (caseDto.Year != null)
                cases.Year = caseDto.Year;

            if (caseDto.LitigationDegree != null)
                cases.LitigationDegree = caseDto.LitigationDegree;

            if (caseDto.Status != null)
                cases.Status = (Enums.Status)caseDto.Status;

            if (caseDto.HearingDtos != null)
                cases.Hearings = caseDto.HearingDtos
                    .Select(dtos => ObjectMapper.Map<HearingDto, Hearing>(dtos))
                    .ToList();

            if (caseDto.LawyerId != null)
                cases.LawyerId = caseDto.LawyerId;


            await _caseRepo.UpdateAsync(cases, autoSave: true);

            return ObjectMapper.Map<Case, CaseDto>(cases);
        }
        /// <summary>
        /// Deletes a case identified by the specified unique identifier.
        /// </summary>
        /// <remarks>This method retrieves the case by its unique identifier and deletes it if it exists.  If the case is
        /// not found, no action is taken, and the method returns <see langword="false"/>.</remarks>
        /// <param name="caseGuid">The unique identifier of the case to delete.</param>
        /// <returns><see langword="true"/> if the case was successfully deleted;  otherwise, <see langword="false"/> if the case does
        /// not exist.</returns>
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
        /// <summary>
        /// Retrieves a paginated and sorted list of cases, including their associated lawyers and hearings.
        /// </summary>
        /// <remarks>This method queries the database for cases, including their related lawyers and
        /// hearings, applies the specified sorting and pagination, and maps the results to DTOs for
        /// consumption.</remarks>
        /// <param name="input">The pagination and sorting parameters, including the number of items to skip, the maximum number of items to
        /// return, and the sorting criteria. The <paramref name="input"/> parameter cannot be null.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result contains a <see
        /// cref="PagedResultDto{T}"/> of <see cref="CaseLawyerHearingsWithNavigationProperty"/>, where each item
        /// includes case details, associated lawyer information, and a list of hearings.</returns>
        public async Task<PagedResultDto<CaseLawyerHearingsWithNavigationProperty>> GetCaseWithLawyersAndHearingsListAsync(PagedAndSortedResultRequestDto input)
        {

            var query = await _caseRepo.GetQueryableAsync();
            query = query.Include(c => c.Lawyer);
            query = query.Include(c => c.Hearings);

            //if (date != null)
            //{
            //    query.Where(x => x.Hearing.Date == date);

            //}

            query = query.OrderBy(input.Sorting ?? "CaseTitle");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);


            var items = await query.Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();


            var result = items.Select(cases => new CaseLawyerHearingsWithNavigationProperty
            {
                CaseDto = ObjectMapper.Map<Case, CaseDto>(cases),
                LawyerDto = cases.Lawyer != null ? ObjectMapper.Map<Lawyer, LawyerDto>(cases.Lawyer) : null,
                HearingDtos = cases.Hearings != null
                ? ObjectMapper.Map<List<Hearing>, List<HearingDto>>(cases.Hearings.ToList())
                : new List<HearingDto>(),

            }).ToList();

            return new PagedResultDto<CaseLawyerHearingsWithNavigationProperty>(totalCount, result);
        }
        /// <summary>
        /// Retrieves a case, along with its associated lawyers and hearings, based on the specified case identifier.
        /// </summary>
        /// <remarks>This method combines data from multiple repositories to provide a comprehensive view of a
        /// case,  including its associated lawyers and hearings. Ensure that the provided <paramref name="caseGuid"/> 
        /// corresponds to an existing case in the system.</remarks>
        /// <param name="caseGuid">The unique identifier of the case to retrieve.</param>
        /// <returns>An instance of <see cref="CaseLawyerHearingsWithNavigationProperty"/> containing the case details,  associated
        /// lawyer information, and a list of related hearings.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if no case or hearing is found for the specified <paramref name="caseGuid"/>.</exception>
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
                CaseDto = ObjectMapper.Map<Case, CaseDto>(cases),
                LawyerDto = ObjectMapper.Map<Lawyer, LawyerDto>(cases.Lawyer),
                HearingDtos = ObjectMapper.Map<List<Hearing>, List<HearingDto>>(cases.Hearings.ToList())
            };
        }
    }
}
