using Inva.LawMax.Samples;
using Xunit;

namespace Inva.LawMax.EntityFrameworkCore.Domains;

[Collection(LawMaxTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LawMaxEntityFrameworkCoreTestModule>
{

}
