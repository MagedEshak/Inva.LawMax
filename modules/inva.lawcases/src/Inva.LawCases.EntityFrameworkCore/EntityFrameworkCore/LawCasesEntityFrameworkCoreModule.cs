using Inva.LawCases.Models;
using Inva.LawCases.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Inva.LawCases.EntityFrameworkCore;

[DependsOn(
    typeof(LawCasesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class LawCasesEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<LawCasesDbContext>(options =>
        {
            options.AddDefaultRepositories<ILawCasesDbContext>(includeAllEntities: true);

            options.AddRepository<Lawyer, LawyerRepositpry>();
            options.AddRepository<Hearing, HearingRepository>();
            options.AddRepository<Case, CaseRepository>();
        });
    }
}
