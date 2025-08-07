using Inva.LawCases.Enums;
using JetBrains.Annotations;
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
    public class Case : AuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete, IHasConcurrencyStamp
    {

        public string Number { get; set; }
        public string CaseTitle { get; set; }
        public string Description { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }
        public Status Status { get; set; }
        public int Year { get; set; }
        public Guid LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
        public Guid? TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }
        public ICollection<Hearing> Hearings { get; set; } = new List<Hearing>();

        //Constructor
        public Case(string number,
            string caseTitle,
            string description,
            string litigationDegree,
            string finalVerdict,
            Status status,
            int year,
            Guid lawyerId,
            Guid? tenantId,
            bool isDeleted,
            string concurrencyStamp
            )
        {
            Number = number;
            CaseTitle = caseTitle;
            Description = description;
            LitigationDegree = litigationDegree;
            FinalVerdict = finalVerdict;
            Status = status;
            Year = year;
            LawyerId = lawyerId;
            TenantId = tenantId;
            IsDeleted = isDeleted;
            ConcurrencyStamp = concurrencyStamp;
            Hearings = new List<Hearing>();
        }


        public Case()
        {
            
        }

    }
}
