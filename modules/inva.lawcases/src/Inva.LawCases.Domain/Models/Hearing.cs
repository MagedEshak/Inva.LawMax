using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Hearing : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Location { get; set; }

        public Guid? TenantId { get; set; }

        public Case? Case { get; set; }

    }
}
