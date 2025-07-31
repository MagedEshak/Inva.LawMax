using Inva.LawCases.DTOs.Case;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.DTOs.Lawyer
{
    public class LawyerWithNavigationPropertyDto
    {
        public LawyerDto Lawyer { get; set; }
        public ICollection<CaseDto> Cases { get; set; } = new List<CaseDto>();
        //public CaseDto Case { get; set; }
    }
}
