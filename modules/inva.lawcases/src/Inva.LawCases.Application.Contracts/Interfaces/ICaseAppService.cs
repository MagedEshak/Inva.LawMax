using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawCases.Interfaces
{
    public interface ICaseAppService
    {
        Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto);
        Task<CaseDto> UpdateCaseAsync(Guid id,CreateUpdateCaseDto caseDto);
        //Task<PagedResultDto<CaseDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<CaseLawyerHearingsWithNavigationProperty>> GetCaseWithLawyersAndHearingsListAsync(PagedAndSortedResultRequestDto input);
      //  Task<PagedResultDto<CaseHearingWithNavigationProperty>> GetCaseWithHearingListAsync(PagedAndSortedResultRequestDto input);
        Task<CaseLawyerHearingsWithNavigationProperty> GetCaseWithLawyersAndHearingsByIdAsync(Guid caseGuid);
       // Task<CaseHearingWithNavigationProperty> GetCaseHearingByIdAsync(Guid caseGuid);
        Task<bool> DeleteCaseAsync(Guid caseGuid);
    }
}
