using Xunit;

namespace Inva.LawMax.EntityFrameworkCore;

[CollectionDefinition(LawMaxTestConsts.CollectionDefinitionName)]
public class LawMaxEntityFrameworkCoreCollection : ICollectionFixture<LawMaxEntityFrameworkCoreFixture>
{

}
