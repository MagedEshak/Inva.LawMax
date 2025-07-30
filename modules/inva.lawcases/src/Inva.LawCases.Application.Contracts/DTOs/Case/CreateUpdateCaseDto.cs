
using Inva.LawCases.Enums;
using Inva.LawMax.DTOs.Lawyer;
using System;

namespace Inva.LawCases.DTOs.Case
{
    public class CreateUpdateCaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public Guid? LawyerId { get; set; }
        public Guid? HearingId { get; set; }
        public Guid? TenantId { get; set; }
        public string? ConcurrencyStamp { get; set; }

    }
}
