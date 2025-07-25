using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inva.LawMax.DTOs.Lawyer
{
    public class LawyerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        
        public Guid? CaseId { get; set; }
    }
}
