using Inva.LawCases.Enums;
using System;

namespace Inva.LawCases.DTOs.Case
{
    public class CaseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public string? LawyerName { get; set; }
        public string? LawyerSpeciality { get; set; }
        public DateTime? HearingDate { get; set; }
        public string? HearingLocation { get; set; }
        public DateTime CreationTime { get; set; }
        public string? ConcurrencyStamp { get; set; }

    }
}
