using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Inva.LawCases;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class LawCasesInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<LawCasesInstallerModule>();
        });
    }
}
