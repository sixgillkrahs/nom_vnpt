using CSP.Category;
using CTIN.Abp.Account;
using CTIN.Abp.AutoMapper;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Modularity;
using CTIN.Abp.NotificationService;
using CTIN.Abp.PermissionManagement;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.TenantManagement;
using CSP.NotificationConfig;
using CTIN.Abp.BlobStoring;
using CSP_NET.Work.ResourseManagement;


namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(WorkApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(NotificationServiceApplicationModule),
    typeof(CategoryApplicationModule)
    )]
[DependsOn(typeof(NotificationConfigApplicationModule))]

    public class WorkApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WorkApplicationModule>();
        });
       

    }

    
}
