using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawCases.DTOs.Lawyer
{
    public class GetLawyerFilterDto:PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
