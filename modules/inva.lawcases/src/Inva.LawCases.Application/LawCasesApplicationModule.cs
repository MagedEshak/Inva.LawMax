using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesDomainModule),
    typeof(LawCasesApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class LawCasesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<LawCasesApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<LawCasesApplicationModule>(validate: true);
        });
    }
}
