using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Case : AuditedAggregateRoot<Guid>, IMultiTenant
    {

        public int Number { get; set; }
        public int Year { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }

        public virtual ICollection<Lawyer> Lawyers { get; set; } = new List<Lawyer>();
        public virtual ICollection<Hearing> Hearings { get; set; } = new List<Hearing>();
        public Guid? TenantId { get; set; }
    }
}
