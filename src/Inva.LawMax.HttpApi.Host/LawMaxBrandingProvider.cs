using Microsoft.Extensions.Localization;
using Inva.LawMax.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Inva.LawMax;

[Dependency(ReplaceServices = true)]
public class LawMaxBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LawMaxResource> _localizer;

    public LawMaxBrandingProvider(IStringLocalizer<LawMaxResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
