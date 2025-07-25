using System.Threading.Tasks;

namespace Inva.LawMax.Data;

public interface ILawMaxDbSchemaMigrator
{
    Task MigrateAsync();
}
