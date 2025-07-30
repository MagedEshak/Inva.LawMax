using Inva.LawCases.DTOs.Hearing;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.DTOs.Case
{
    public class CaseHearingWithNavigationProperty
    {
        public CaseDto CaseDto { get; set; }
        public HearingDto HearingDto { get; set; }
    }
}
