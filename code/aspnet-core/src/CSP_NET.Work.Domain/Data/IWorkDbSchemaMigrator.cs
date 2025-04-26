using System.Threading.Tasks;

namespace CSP_NET.Work.Data;

public interface IWorkDbSchemaMigrator
{
    Task MigrateAsync();
}
