using Inva.LawCases.DTOs.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Inva.LawMax.DTOs.Lawyer
{
    public class CreateUpdateLawyerDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Speciality { get; set; }
        public ICollection<CaseDto>? Cases { get; set; } = new List<CaseDto>();
        public string? ConcurrencyStamp { get; set; }
    }
}
