using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Configuration
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            throw new NotImplementedException();
        }
    }
}
