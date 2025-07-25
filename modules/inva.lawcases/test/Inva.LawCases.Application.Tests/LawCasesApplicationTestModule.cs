using Volo.Abp.Modularity;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesApplicationModule),
    typeof(LawCasesDomainTestModule)
    )]
public class LawCasesApplicationTestModule : AbpModule
{

}
