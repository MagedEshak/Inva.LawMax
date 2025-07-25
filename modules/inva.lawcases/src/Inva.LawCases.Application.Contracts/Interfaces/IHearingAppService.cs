using Inva.LawCases.DTOs.Hearing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Interfaces
{
    public interface IHearingAppService
    {
        Task<HearingDto> CreateHearingAsync(CreateUpdateHearingDto hearingDto);
        Task<HearingDto> UpdateHearingAsync(CreateUpdateHearingDto hearingDto);
        Task<HearingDto> GetHearingByIdAsync(Guid hearingGuid);
        Task<bool> DeleteHearingAsync(Guid hearingGuid);
    }
}
