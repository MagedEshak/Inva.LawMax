using Inva.LawCases.DTOs.Case;
using Inva.LawCases.DTOs.Hearing;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawCases.Interfaces
{
    public interface IHearingAppService
    {
        Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearingDto);
        Task<HearingDto> UpdateHearingAsync(Guid id ,CreateUpdateHearingDto hearingDto);
        Task<PagedResultDto<HearingWithNavigationPropertyDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<HearingWithNavigationPropertyDto> GetHearingByIdAsync(Guid hearingGuid);
        Task<List<HearingDto>> GetHearingsByLawyerAsync(Guid lawyerId);
        Task<bool> DeleteHearingAsync(Guid hearingGuid);
    }
}
