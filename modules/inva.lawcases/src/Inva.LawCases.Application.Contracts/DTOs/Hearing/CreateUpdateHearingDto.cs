using System;

namespace Inva.LawCases.DTOs.Hearing
{
    public class CreateUpdateHearingDto
    {
        public DateTime? Date { get; set; } = DateTime.Now;
        public string? Location { get; set; }
        public string Decision { get; set; }
        public Guid? CaseId { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
