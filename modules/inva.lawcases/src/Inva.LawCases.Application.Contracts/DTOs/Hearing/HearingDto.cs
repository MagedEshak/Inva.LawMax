using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawCases.DTOs.Hearing
{
    public class HearingDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Decision { get; set; }

        public Guid? CaseId { get; set; }
    }
}
