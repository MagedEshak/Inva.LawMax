using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class LawCasesApplicationContractsModule : AbpModule
{

}
