using Inva.LawCases.Enums;
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

        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }


        public Guid? LawyerId { get; set; }
        public Guid? HearingId { get; set; }
        public Guid? TenantId { get; set; }


        public Lawyer? Lawyer { get; set; }
        public Hearing? Hearing { get; set; }


    }
}
