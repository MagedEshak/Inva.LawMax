using Inva.LawCases.DTOs.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Inva.LawCases.Interfaces
{
    public interface ICaseAppService
    {
        Task<CaseDto> CreateCaseAsync(CreateUpdateCaseDto caseDto);
        Task<CaseDto> UpdateCaseAsync(CreateUpdateCaseDto caseDto);
        Task<CaseDto> GetCaseByIdAsync(Guid caseGuid);
        Task<bool> DeleteCaseAsync(Guid caseGuid);
    }
}
