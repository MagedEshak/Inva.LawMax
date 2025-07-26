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
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Speciality { get; set; }


        public Case? Case { get; set; }

        public Guid? TenantId { get; set; }
        public Lawyer() : base()
        {
            
        }
        public Lawyer(string name, string email, string phone, string address, string speciality, Case? @case, Guid? tenantId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Speciality = speciality;
            Case = @case;
            TenantId = tenantId;
            
        }

    }
}
