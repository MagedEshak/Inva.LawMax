using Inva.LawMax.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawMax.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LawMaxController : AbpControllerBase
{
    protected LawMaxController()
    {
        LocalizationResource = typeof(LawMaxResource);
    }
}
