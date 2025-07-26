using Inva.LawCases.DTOs.Hearing;
using Inva.LawCases.Enums;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawCases.DTOs.Case
{
    public class CaseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }


        public Guid? LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerEmail { get; set; }
        public string LawyerPhone { get; set; }
        public string LawyerSpeciality { get; set; }

        public Guid? HearingId { get; set; }
        public DateTime HearingDate { get; set; } = DateTime.Now;
        public string HearingLocation { get; set; }

    }
}
