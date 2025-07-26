
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawCases.Interfaces
{
    public interface ILawyerAppService
    {
        Task<LawyerDto> CreateLawyerAsync(CreateUpdateLawyerDto LawyerDto);
        Task<LawyerDto> UpdateLawyerAsync(Guid id, CreateUpdateLawyerDto LawyerDto);
        Task<PagedResultDto<LawyerDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<LawyerDto> GetLawyerByIdAsync(Guid LawyerGuid);
        Task<bool> DeleteLawyerAsync(Guid LawyerGuid);
    }
}
