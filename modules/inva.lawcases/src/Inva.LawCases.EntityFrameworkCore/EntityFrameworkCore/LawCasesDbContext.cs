using Inva.LawCases.Configuration;
using Inva.LawCases.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawCases.EntityFrameworkCore;

[ConnectionStringName(LawCasesDbProperties.ConnectionStringName)]
public class LawCasesDbContext : AbpDbContext<LawCasesDbContext>, ILawCasesDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<Lawyer> Lawyers { get; set; }
    public DbSet<Hearing> Hearings { get; set; }
    public DbSet<Case> Cases { get; set; }


    public LawCasesDbContext(DbContextOptions<LawCasesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureLawCases();


        builder.ApplyConfiguration(new LawyerConfiguration());
        builder.ApplyConfiguration(new HearingConfiguration());

    }
}
