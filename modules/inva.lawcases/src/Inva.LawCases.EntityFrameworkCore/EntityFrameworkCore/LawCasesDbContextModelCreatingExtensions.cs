using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Inva.LawCases.EntityFrameworkCore;

public static class LawCasesDbContextModelCreatingExtensions
{
    public static void ConfigureLawCases(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        //Configure all entities here. Example:

        builder.Entity<Lawyer>(b =>
        {
            //Configure table & schema name
            b.ConfigureByConvention();

            //Properties
            b.HasIndex(e => e.Email).IsUnique();
            b.HasIndex(e => e.Phone).IsUnique();
        });

        builder.Entity<Case>(b =>
        {
            //Configure table & schema name
            b.ConfigureByConvention();

            //Properties
            b.HasAlternateKey(e => e.Number);

        });

    }
}
