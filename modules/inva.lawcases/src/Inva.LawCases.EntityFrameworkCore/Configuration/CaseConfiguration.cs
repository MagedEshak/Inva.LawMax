using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Inva.LawCases.Configuration
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Cases");

            // to set general value automatic
            builder.ConfigureByConvention();

            builder.HasOne(l => l.Lawyer)
                 .WithMany(c => c.Cases)
                 .HasForeignKey(l => l.LawyerId)
                 .OnDelete(DeleteBehavior.Cascade); // عند حذف Lawyer، سيتم حذف كل القضايا المرتبطة
        }
    }
}