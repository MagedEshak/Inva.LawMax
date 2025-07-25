using Inva.LawCases.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawCases;

public abstract class LawCasesController : AbpControllerBase
{
    protected LawCasesController()
    {
        LocalizationResource = typeof(LawCasesResource);
    }
}
