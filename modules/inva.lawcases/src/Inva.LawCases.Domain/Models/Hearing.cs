using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Hearing : FullAuditedEntity<Guid>,IMultiTenant
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Decision { get; set; }

        public Guid CaseId { get; set; }
        public virtual Case Case { get; set; }

        public Guid? TenantId { get; set; }
    }
}
