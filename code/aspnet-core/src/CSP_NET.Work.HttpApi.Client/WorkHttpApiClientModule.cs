using Microsoft.Extensions.DependencyInjection;
using CTIN.Abp.Account;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Modularity;
using CTIN.Abp.PermissionManagement;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.VirtualFileSystem;
using CTIN.Abp.NotificationService;
using CSP.NotificationConfig;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkApplicationContractsModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule),
    typeof(NotificationServiceHttpApiClientModule)
)]
[DependsOn(typeof(NotificationConfigHttpApiClientModule))]
public class WorkHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WorkApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WorkHttpApiClientModule>();
        });
    }
}
