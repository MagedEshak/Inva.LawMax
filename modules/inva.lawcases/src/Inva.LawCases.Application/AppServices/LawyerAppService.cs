
using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.DTOs.Lawyer;
using Inva.LawCases.Interfaces;
using Inva.LawCases.IRepositories;
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
    /// <summary>
    /// Provides application-level services for managing lawyers, including operations such as creating, updating,
    /// deleting, and retrieving lawyer information.
    /// </summary>
    /// <remarks>This service acts as a bridge between the domain layer and the application layer, exposing
    /// methods for managing lawyer entities and their related data. It includes functionality for checking the
    /// uniqueness of email and phone numbers, handling pagination, and managing relationships between lawyers and their
    /// associated cases.</remarks>
    public class LawyerAppService : ApplicationService, ILawyerAppService
    {
        private readonly ILawyerRepository _lawyerRepo;
        public LawyerAppService(ILawyerRepository lawyerRepo)
        {
            _lawyerRepo = lawyerRepo;
        }
        /// <summary>
        /// Asynchronously checks whether the specified email exists in the system.
        /// </summary>
        /// <param name="email">The email address to check. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is  true if the email exists; otherwise,
        /// false.</returns> 
        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _lawyerRepo.CheckEmailAsync(email);
        }
        /// <summary>
        /// Checks whether the specified phone number exists in the system.
        /// </summary>
        /// <param name="phone">The phone number to check. Must be a non-null, non-empty string.</param>
        /// <returns><see langword="true"/> if the phone number exists; otherwise, <see langword="false"/>.</returns> 
        public async Task<bool> CheckPhoneAsync(string phone)
        {
            return await _lawyerRepo.CheckPhoneAsync(phone);
        }
        /// <summary>
        /// Creates a new lawyer record asynchronously and returns the created lawyer's details.
        /// </summary>
        /// <remarks>This method maps the provided <paramref name="lawyerDto"/> to a lawyer entity,
        /// inserts it into the repository, and returns the created lawyer's details as a <see cref="LawyerDto"/>. The
        /// operation is performed asynchronously.</remarks>
        /// <param name="lawyerDto">An object containing the details of the lawyer to be created. This parameter must not be null.</param>
        /// <returns>A <see cref="LawyerDto"/> object representing the newly created lawyer.</returns> 
        public async Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto lawyerDto)
        {
            try
            {
                var lawyerEntity = ObjectMapper.Map<CreateUpdateLawyerDto, Lawyer>(lawyerDto);

                var insertedLawyer = await _lawyerRepo.InsertAsync(lawyerEntity, autoSave: true);

                return ObjectMapper.Map<Lawyer, LawyerDto>(insertedLawyer);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in CreateLawyerAsync");
                throw;
            }
        }
         /// <summary>
        /// Deletes a lawyer identified by the specified unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to locate and delete the lawyer.  If
        /// the lawyer does not exist, no action is taken, and the method returns <see langword="false"/>.</remarks>
        /// <param name="lawyerGuid">The unique identifier of the lawyer to delete.</param>
        /// <returns><see langword="true"/> if the lawyer was successfully deleted;  otherwise, <see langword="false"/> if no
        /// lawyer with the specified identifier was found.</returns>
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
        /// <summary>
        /// Retrieves a paginated and sorted list of lawyers, including their associated cases and hearings.
        /// </summary>
        /// <remarks>This method retrieves lawyers from the database along with their related cases and
        /// hearings. The results are paginated and sorted based on the provided <paramref name="input"/> parameters. If
        /// no sorting is specified, the default sorting is by the lawyer's name.</remarks>
        /// <param name="input">The pagination and sorting parameters, including the number of items to skip, the maximum number of items to
        /// return, and the sorting criteria. The <paramref name="input"/> parameter cannot be null.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a <see
        /// cref="PagedResultDto{T}"/> of <see cref="LawyerWithNavigationPropertyDto"/>, where each item includes a
        /// lawyer and their associated cases.</returns> 
        public async Task<PagedResultDto<LawyerWithNavigationPropertyDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _lawyerRepo.GetQueryableAsync();
            query = query.Include(c => c.Cases).ThenInclude(h => h.Hearings);

            query = query.OrderBy(input.Sorting ?? "Name");

            // إجمالي العناصر قبل التصفية
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Pagination

            var items = await query.Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();
            var result = items.Select(lawyer => new LawyerWithNavigationPropertyDto
            {
                Lawyer = ObjectMapper.Map<Lawyer, LawyerDto>(lawyer),
                Cases = lawyer.Cases != null ?
                ObjectMapper.Map<List<Case>, List<CaseDto>>(lawyer.Cases.ToList())
                : new List<CaseDto>()
            }).ToList();
            return new PagedResultDto<LawyerWithNavigationPropertyDto>(totalCount, result);
        }


        public async Task<LawyerWithNavigationPropertyDto> GetLawyerByIdAsync(Guid ID, DateTime? date)
        {
            var lawyer = await _lawyerRepo.GetLawyerWithCase(ID);

            if (lawyer == null)
            {
                throw new EntityNotFoundException("Lawyer Not Found");
            }
            var filteredCases = lawyer.Cases?.ToList() ?? new List<Case>();

            if (date.HasValue)
            {
                // فلترة القضايا حسب وجود جلسة لها بنفس التاريخ المحدد
                filteredCases = filteredCases
                    .Where(c => c.Hearings != null &&
                                c.Hearings.Any(h => h.Date.Date == date.Value.Date))
                    .ToList();
            }
            return new LawyerWithNavigationPropertyDto
            {
                Lawyer = ObjectMapper.Map<Lawyer, LawyerDto>(lawyer),
                Cases = ObjectMapper.Map<List<Case>, List<CaseDto>>(filteredCases)
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

            if (lawyerDto.Cases != null)
            {
                lawyer.Cases = lawyerDto.Cases
                    .Select(dto => ObjectMapper.Map<CaseDto, Case>(dto))
                    .ToList();
            }


            await _lawyerRepo.UpdateAsync(lawyer, autoSave: true);

            return ObjectMapper.Map<Lawyer, LawyerDto>(lawyer);
        }

    }
}
