using Inva.LawMax.Localization;
using Volo.Abp.Application.Services;

namespace Inva.LawMax;

/* Inherit your application services from this class.
 */
public abstract class LawMaxAppService : ApplicationService
{
    protected LawMaxAppService()
    {
        LocalizationResource = typeof(LawMaxResource);
    }
}
