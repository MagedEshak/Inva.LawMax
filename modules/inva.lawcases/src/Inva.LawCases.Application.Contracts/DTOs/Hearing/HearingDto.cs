using Inva.LawCases.Enums;
using System;

namespace Inva.LawCases.DTOs.Hearing
{
    public class HearingDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Location { get; set; }
        public string Decision { get; set; }
        public Guid? CaseId { get; set; }
        public string? CaseTitle { get; set; }
        public string? CaseDescription { get; set; }
        public string? CaseLitigationDegree { get; set; }
        public string? CaseFinalVerdict { get; set; }
        public Status? CaseStatus { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
