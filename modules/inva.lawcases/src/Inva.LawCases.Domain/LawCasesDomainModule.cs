using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Inva.LawCases;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(LawCasesDomainSharedModule)
)]
public class LawCasesDomainModule : AbpModule
{

}
