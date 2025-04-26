using CSP.Category;
using CTIN.Abp.Account;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Modularity;
using CTIN.Abp.NotificationService;
using CTIN.Abp.ObjectExtending;
using CTIN.Abp.PermissionManagement;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.TenantManagement;
using CSP.NotificationConfig;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkDomainSharedModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(NotificationServiceApplicationContractsModule),
    typeof(AbpObjectExtendingModule),
    typeof(CategoryApplicationContractsModule)
)]
[DependsOn(typeof(NotificationConfigApplicationContractsModule))]
public class WorkApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        WorkDtoExtensions.Configure();
    }
}
