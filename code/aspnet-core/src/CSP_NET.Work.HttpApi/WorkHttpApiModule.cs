using Localization.Resources.AbpUi;
using CSP_NET.Work.Localization;
using CTIN.Abp.Account;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Localization;
using CTIN.Abp.Modularity;
using CTIN.Abp.PermissionManagement.HttpApi;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.NotificationService;
using CSP.Category;
using CSP.NotificationConfig;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(NotificationServiceHttpApiModule),
    typeof(CategoryHttpApiModule)
    )]
[DependsOn(typeof(NotificationConfigHttpApiModule))]
public class WorkHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WorkResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
