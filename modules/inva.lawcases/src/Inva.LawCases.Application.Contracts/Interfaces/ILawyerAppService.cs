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
    public interface ILawyerAppService:ICrudAppService<LawyerDto,Guid, PagedAndSortedResultRequestDto,CreateUpdateLawyerDto>
    {
     
    }
}
