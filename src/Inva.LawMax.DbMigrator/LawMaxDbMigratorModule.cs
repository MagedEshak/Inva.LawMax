using Inva.LawMax.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Inva.LawMax.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LawMaxEntityFrameworkCoreModule),
    typeof(LawMaxApplicationContractsModule)
)]
public class LawMaxDbMigratorModule : AbpModule
{
}
