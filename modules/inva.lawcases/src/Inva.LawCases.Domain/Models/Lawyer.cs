using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Inva.LawCases.Models
{
    public class Lawyer : AuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Speciality { get; set; }
        public Guid? TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }
        public ICollection<Case> Cases { get; set; } = new List<Case>();

        //Constructor
        public Lawyer(string name,
            string email, string phone,
            string address,
            string speciality,
            Guid? tenantId,
            bool isDeleted, string concurrencyStamp)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Speciality = speciality;
            TenantId = tenantId;
            IsDeleted = isDeleted;
            ConcurrencyStamp = concurrencyStamp;
            Cases = new List<Case>();
        }

        public Lawyer()
        {
            
        }

    }
}
