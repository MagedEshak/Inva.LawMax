using Volo.Abp.Modularity;

namespace Inva.LawMax;

/* Inherit from this class for your domain layer tests. */
public abstract class LawMaxDomainTestBase<TStartupModule> : LawMaxTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
