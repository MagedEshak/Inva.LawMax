using Inva.LawCases.Localization;
using Volo.Abp.Application.Services;

namespace Inva.LawCases;

public abstract class LawCasesAppService : ApplicationService
{
    protected LawCasesAppService()
    {
        LocalizationResource = typeof(LawCasesResource);
        ObjectMapperContext = typeof(LawCasesApplicationModule);
    }
}
