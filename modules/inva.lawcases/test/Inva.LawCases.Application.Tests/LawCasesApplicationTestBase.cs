using Volo.Abp.Modularity;

namespace Inva.LawCases;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class LawCasesApplicationTestBase<TStartupModule> : LawCasesTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
