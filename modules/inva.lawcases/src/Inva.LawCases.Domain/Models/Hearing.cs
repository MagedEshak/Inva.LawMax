using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Hearing : AuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete, IHasConcurrencyStamp
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Location { get; set; }
        public string Decision { get; set; }
        public Guid? CaseId { get; set; }
        public virtual Case? Case { get; set; }
        public Guid? TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }

        public Hearing(DateTime date,
            string location, 
            string decision, 
            Guid? caseId, 
            Guid? tenantId, 
            bool isDeleted, 
            string concurrencyStamp)
        {
            Date = date;
            Location = location;
            Decision = decision;
            CaseId = caseId;
            TenantId = tenantId;
            IsDeleted = isDeleted;
            ConcurrencyStamp = concurrencyStamp;
        }

        public Hearing()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
    }
}
