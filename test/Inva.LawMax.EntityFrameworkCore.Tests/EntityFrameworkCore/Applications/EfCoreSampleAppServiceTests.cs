using Inva.LawMax.Samples;
using Xunit;

namespace Inva.LawMax.EntityFrameworkCore.Applications;

[Collection(LawMaxTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LawMaxEntityFrameworkCoreTestModule>
{

}
