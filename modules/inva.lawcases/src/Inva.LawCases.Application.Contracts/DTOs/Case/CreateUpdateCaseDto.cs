
using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Enums;
using System;
using System.Collections.Generic;

namespace Inva.LawCases.DTOs.Case
{
    public class CreateUpdateCaseDto
    {
        public int Number { get; set; }
        public string CaseTitle { get; set; }
        public string Description { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }
        public Status Status { get; set; }
        public int Year { get; set; }
        public Guid LawyerId { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public ICollection<HearingDto> HearingDtos { get; set; } = new List<HearingDto>();
    }
}
