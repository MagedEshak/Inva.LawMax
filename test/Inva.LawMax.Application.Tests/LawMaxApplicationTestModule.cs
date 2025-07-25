using Volo.Abp.Modularity;

namespace Inva.LawMax;

[DependsOn(
    typeof(LawMaxApplicationModule),
    typeof(LawMaxDomainTestModule)
)]
public class LawMaxApplicationTestModule : AbpModule
{

}
