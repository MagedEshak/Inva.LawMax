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
    public class CreateUpdateCaseDto
    {
        public int Number { get; set; }
        public int Year { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }


        public List<Guid> LawyerIds { get; set; } = new();
        public List<CreateUpdateHearingDto> Hearings { get; set; } = new();
    }
}
