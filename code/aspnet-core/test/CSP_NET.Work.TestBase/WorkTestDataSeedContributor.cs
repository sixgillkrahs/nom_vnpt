using System.Threading.Tasks;
using CTIN.Abp.Data;
using CTIN.Abp.DependencyInjection;

namespace CSP_NET.Work;

public class WorkTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */

        return Task.CompletedTask;
    }
}
