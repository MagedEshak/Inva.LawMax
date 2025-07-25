using Inva.LawCases.DTOs.Hearing;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawCases.DTOs.Case
{
    public class CaseDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }

        public virtual ICollection<LawyerDto> Lawyers { get; set; } = new List<LawyerDto>();
        public virtual ICollection<HearingDto> Hearings { get; set; } = new List<HearingDto>();

    }
}
