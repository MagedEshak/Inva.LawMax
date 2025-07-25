using Volo.Abp.Modularity;

namespace Inva.LawCases;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class LawCasesDomainTestBase<TStartupModule> : LawCasesTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
