using Volo.Abp.Modularity;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesDomainModule),
    typeof(LawCasesTestBaseModule)
)]
public class LawCasesDomainTestModule : AbpModule
{

}
