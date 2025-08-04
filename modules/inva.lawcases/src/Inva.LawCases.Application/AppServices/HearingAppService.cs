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
    /// <summary>
    /// Provides application-level services for managing hearings, including operations such as  retrieving, creating,
    /// updating, and deleting hearings, as well as retrieving hearings  associated with specific cases or lawyers.
    /// </summary>
    /// <remarks>This service acts as a bridge between the domain layer and the application layer,  exposing
    /// methods to interact with hearing-related data. It supports operations such as  pagination, sorting, and
    /// navigation property inclusion for related entities like cases  and lawyers.</remarks>
    public class HearingAppService : BaseApplicationService, IHearingAppService
    {
        private readonly IHearingRepository _hearingRepo;

        public HearingAppService(IHearingRepository hearingRepo)
        {
            _hearingRepo = hearingRepo;
        }
        /// <summary>
        /// Retrieves a paginated and sorted list of hearings, including related case and lawyer information.
        /// </summary>
        /// <remarks>The method applies the specified sorting and pagination to the query. If no sorting
        /// is provided,  the default sorting is applied based on the "Location" property. The returned data includes 
        /// navigation properties for the related case and lawyer entities.</remarks>
        /// <param name="input">The pagination and sorting parameters for the query.  <see cref="PagedAndSortedResultRequestDto.SkipCount"/>
        /// specifies the number of items to skip,  <see cref="PagedAndSortedResultRequestDto.MaxResultCount"/>
        /// specifies the maximum number of items to return,  and <see cref="PagedAndSortedResultRequestDto.Sorting"/>
        /// specifies the sorting criteria.</param>
        /// <returns>A <see cref="PagedResultDto{T}"/> containing the total count of items and a list of  <see
        /// cref="HearingWithNavigationPropertyDto"/> objects representing the hearings and their related data.</returns>
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
        /// <summary>
        /// Retrieves a hearing by its unique identifier, including related case details.
        /// </summary>
        /// <remarks>This method retrieves a hearing along with its associated case details from the data
        /// source. If the hearing does not exist, an <see cref="EntityNotFoundException"/> is thrown.</remarks>
        /// <param name="id">The unique identifier of the hearing to retrieve.</param>
        /// <returns>A <see cref="HearingWithNavigationPropertyDto"/> containing the hearing details and its associated case
        /// information.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if no hearing with the specified <paramref name="id"/> is found.</exception>
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
        /// <summary>
        /// Retrieves a list of hearings associated with the specified lawyer.
        /// </summary>
        /// <param name="lawyerId">The unique identifier of the lawyer whose hearings are to be retrieved.</param>
        /// <returns>A list of <see cref="HearingDto"/> objects representing the hearings associated with the specified lawyer.
        /// If no hearings are found, an empty list is returned.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when no hearings are found for the specified <paramref name="lawyerId"/>.</exception>
        public async Task<List<HearingDto>> GetHearingsByLawyerAsync(Guid lawyerId)
        {
            var hearing = await _hearingRepo.GetHearingsByLawyer(lawyerId);

            if (hearing == null)
            {
                throw new EntityNotFoundException("Hearing Not Found");
            }
            return hearing;
        }
        /// <summary>
        /// Creates a new hearing and returns the created hearing details.
        /// </summary>
        /// <remarks>This method maps the provided <see cref="CreateUpdateHearingDto"/> to a hearing
        /// entity,  inserts it into the repository, and returns the created hearing as a <see
        /// cref="HearingDto"/>.</remarks>
        /// <param name="hearing">The data transfer object containing the details of the hearing to create.</param>
        /// <returns>A <see cref="HearingDto"/> representing the newly created hearing.</returns>
        public async Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearing)
        {
            var hearingEntity = ObjectMapper.Map<CreateUpdateHearingDto, Hearing>(hearing);

            var insertedHearing = await _hearingRepo.InsertAsync(hearingEntity, autoSave: true);

            return ObjectMapper.Map<Hearing, HearingDto>(insertedHearing);
        }
        /// <summary>
        /// Updates an existing hearing with the specified details.
        /// </summary>
        /// <remarks>This method updates the hearing's date, location, and decision if the corresponding
        /// properties in <paramref name="hearingDto"/> are provided. The changes are persisted to the
        /// database.</remarks>
        /// <param name="id">The unique identifier of the hearing to update.</param>
        /// <param name="hearingDto">An object containing the updated details of the hearing.  The <see
        /// cref="CreateUpdateHearingDto.ConcurrencyStamp"/> must match the current concurrency stamp of the hearing.</param>
        /// <returns>A <see cref="HearingDto"/> object representing the updated hearing.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if a hearing with the specified <paramref name="id"/> is not found.</exception>
        /// <exception cref="AbpDbConcurrencyException">Thrown if the <paramref name="hearingDto"/> contains a concurrency stamp that does not match the current
        /// concurrency stamp of the hearing.</exception>
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
        /// <summary>
        /// Deletes a hearing record identified by the specified unique identifier.
        /// </summary>
        /// <param name="hearing">The unique identifier of the hearing to delete.</param>
        /// <returns><see langword="true"/> if the hearing was successfully deleted;  otherwise, <see langword="false"/> if the
        /// hearing does not exist. </returns> 
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
