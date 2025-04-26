using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Autofac;
using CTIN.Abp.Caching;
using CTIN.Abp.Caching.StackExchangeRedis;
using CTIN.Abp.Modularity;

namespace CSP_NET.Work.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(WorkEntityFrameworkCoreModule),
    typeof(WorkApplicationContractsModule)
    )]
public class WorkDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Work:"; });
    }
}
