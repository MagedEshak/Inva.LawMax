using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Inva.LawCases;

[DependsOn(
    typeof(LawCasesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class LawCasesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(LawCasesApplicationContractsModule).Assembly,
            LawCasesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<LawCasesHttpApiClientModule>();
        });

    }
}
