using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Inva.LawMax.Data;
using Volo.Abp.DependencyInjection;

namespace Inva.LawMax.EntityFrameworkCore;

public class EntityFrameworkCoreLawMaxDbSchemaMigrator
    : ILawMaxDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLawMaxDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the LawMaxDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LawMaxDbContext>()
            .Database
            .MigrateAsync();
    }
}
