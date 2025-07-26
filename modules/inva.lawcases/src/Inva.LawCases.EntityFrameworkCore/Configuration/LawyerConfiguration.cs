using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Inva.LawCases.Configuration
{
    public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {
            builder.ToTable("Lawyers");

            // to set general value automatic
            builder.ConfigureByConvention();
         
            builder.Property(n=>n.Name).HasMaxLength(20);
  
        }
    }
}
