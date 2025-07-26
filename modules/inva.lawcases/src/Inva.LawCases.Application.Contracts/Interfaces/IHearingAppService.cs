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
        Task<PagedResultDto<HearingDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<HearingDto> GetHearingByIdAsync(Guid hearingGuid);
        Task<bool> DeleteHearingAsync(Guid hearingGuid);
    }
}
