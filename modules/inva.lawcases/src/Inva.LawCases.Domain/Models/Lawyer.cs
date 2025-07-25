using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Lawyer : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public Guid? CaseId { get; set; }
        public virtual Case Case { get; set; }

        public Guid? TenantId {  get; set; }
    }
}
