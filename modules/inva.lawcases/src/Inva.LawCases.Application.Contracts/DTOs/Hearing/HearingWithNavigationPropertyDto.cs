using Inva.LawCases.DTOs.Case;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.DTOs.Hearing
{
    public class HearingWithNavigationPropertyDto
    {
        public HearingDto Hearing { get; set; }
        public CaseDto Case { get; set; }
    }
}
