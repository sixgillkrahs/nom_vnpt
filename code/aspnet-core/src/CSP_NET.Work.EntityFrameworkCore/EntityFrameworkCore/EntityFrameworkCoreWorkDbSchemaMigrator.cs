using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CSP_NET.Work.Data;
using CTIN.Abp.DependencyInjection;

namespace CSP_NET.Work.EntityFrameworkCore;

public class EntityFrameworkCoreWorkDbSchemaMigrator
    : IWorkDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWorkDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WorkDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WorkDbContext>()
            .Database
            .MigrateAsync();
    }
}
