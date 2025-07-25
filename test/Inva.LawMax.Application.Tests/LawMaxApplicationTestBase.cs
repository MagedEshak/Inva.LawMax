using Volo.Abp.Modularity;

namespace Inva.LawMax;

public abstract class LawMaxApplicationTestBase<TStartupModule> : LawMaxTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
