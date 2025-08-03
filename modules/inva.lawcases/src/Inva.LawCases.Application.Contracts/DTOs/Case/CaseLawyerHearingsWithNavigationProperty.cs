using Inva.LawCases.DTOs.Hearing;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.DTOs.Case
{
    public class CaseLawyerHearingsWithNavigationProperty
    {
        public CaseDto CaseDto { get; set; }
        public LawyerDto? LawyerDto { get; set; }
        public ICollection<HearingDto>? HearingDtos { get; set; } = new List<HearingDto>();
    }
}
