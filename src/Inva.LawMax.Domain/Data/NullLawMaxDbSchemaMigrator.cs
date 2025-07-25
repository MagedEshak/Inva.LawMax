using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Inva.LawMax.Data;

/* This is used if database provider does't define
 * ILawMaxDbSchemaMigrator implementation.
 */
public class NullLawMaxDbSchemaMigrator : ILawMaxDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
