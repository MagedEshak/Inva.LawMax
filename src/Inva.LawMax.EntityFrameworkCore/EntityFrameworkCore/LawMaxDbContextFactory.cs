using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Inva.LawMax.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class LawMaxDbContextFactory : IDesignTimeDbContextFactory<LawMaxDbContext>
{
    public LawMaxDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        LawMaxEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<LawMaxDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new LawMaxDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Inva.LawMax.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
