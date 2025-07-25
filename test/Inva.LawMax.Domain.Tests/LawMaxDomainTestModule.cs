using Volo.Abp.Modularity;

namespace Inva.LawMax;

[DependsOn(
    typeof(LawMaxDomainModule),
    typeof(LawMaxTestBaseModule)
)]
public class LawMaxDomainTestModule : AbpModule
{

}
