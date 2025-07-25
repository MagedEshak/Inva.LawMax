using Inva.LawMax.Books;
using Xunit;

namespace Inva.LawMax.EntityFrameworkCore.Applications.Books;

[Collection(LawMaxTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<LawMaxEntityFrameworkCoreTestModule>
{

}