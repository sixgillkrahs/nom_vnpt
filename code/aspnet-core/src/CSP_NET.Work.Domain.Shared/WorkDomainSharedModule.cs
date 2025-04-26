using CSP_NET.Work.Localization;
using CTIN.Abp.AuditLogging;
using CTIN.Abp.BackgroundJobs;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Localization;
using CTIN.Abp.Localization.ExceptionHandling;
using CTIN.Abp.Modularity;
using CTIN.Abp.OpenIddict;
using CTIN.Abp.PermissionManagement;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.Validation.Localization;
using CTIN.Abp.VirtualFileSystem;
using CTIN.Abp.NotificationService;
using CSP.Category;
using CSP.NotificationConfig;

namespace CSP_NET.Work;

[DependsOn(
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpBackgroundJobsDomainSharedModule),
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpOpenIddictDomainSharedModule),
    typeof(AbpPermissionManagementDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpTenantManagementDomainSharedModule),
    typeof(NotificationServiceDomainSharedModule),
    typeof(CategoryDomainSharedModule)
    )]
[DependsOn(typeof(NotificationConfigDomainSharedModule))]
public class WorkDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        WorkGlobalFeatureConfigurator.Configure();
        WorkModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WorkDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<WorkResource>("vi")
                .AddVirtualJson("/Localization/Work")
                .AddVirtualJson("/Localization/WorkEx")
                .AddVirtualJson("/Localization/WorkPm")
                .AddVirtualJson("/Localization/WorkScope")
                .AddVirtualJson("/Localization/WorkKw")
                .AddBaseTypes(typeof(AbpValidationResource));

            options.DefaultResourceType = typeof(WorkResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("WorkEx", typeof(WorkResource));
        });
    }
}
