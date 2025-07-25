using Localization.Resources.AbpUi;
using Inva.LawCases.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class LawCasesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(LawCasesHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<LawCasesResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
