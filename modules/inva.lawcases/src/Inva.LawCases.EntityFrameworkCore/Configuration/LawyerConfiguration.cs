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
    public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {

            builder.ToTable("Lawyers");

            builder.Property(n=>n.Name).HasMaxLength(20);
  
            builder.HasOne(l => l.Case)
                   .WithMany(c => c.Lawyers)
                   .HasForeignKey(l => l.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
