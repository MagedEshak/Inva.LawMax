using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawCases.EntityFrameworkCore;

[ConnectionStringName(LawCasesDbProperties.ConnectionStringName)]
public interface ILawCasesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
