using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Configuration
{
    public class HearingConfiguration : IEntityTypeConfiguration<Hearing>
    {
        public void Configure(EntityTypeBuilder<Hearing> builder)
        {
            builder.ToTable("Hearings");

            builder.HasOne(l => l.Case)
                   .WithMany(c => c.Hearings)
                   .HasForeignKey(l => l.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
