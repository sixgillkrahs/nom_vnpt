using System.Threading.Tasks;
using CTIN.Abp.DependencyInjection;

namespace CSP_NET.Work.Data;

/* This is used if database provider does't define
 * IWorkDbSchemaMigrator implementation.
 */
public class NullWorkDbSchemaMigrator : IWorkDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
