using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CSP_NET.Work.MultiTenancy;
using CTIN.Abp.AuditLogging;
using CTIN.Abp.BackgroundJobs;
using CTIN.Abp.Emailing;
using CTIN.Abp.FeatureManagement;
using CTIN.Abp.Identity;
using CTIN.Abp.Localization;
using CTIN.Abp.Modularity;
using CTIN.Abp.MultiTenancy;
using CTIN.Abp.OpenIddict;
using CTIN.Abp.PermissionManagement.Identity;
using CTIN.Abp.PermissionManagement.OpenIddict;
using CTIN.Abp.SettingManagement;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.NotificationService;
using CSP.Category;
using CTIN.Abp.Emailing.MailSettingStore;
using CSP.NotificationConfig;


namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(NotificationServiceDomainModule),
    typeof(AbpEmailingModule),
    typeof(CategoryDomainModule)

)]
[DependsOn(typeof(NotificationConfigDomainModule))]

    public class WorkDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
            options.Languages.Add(new LanguageInfo("vi", "vi", "Tiếng Việt"));
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        context.Services.Replace(ServiceDescriptor.Singleton<IMailSettingStore, NullMailSettingStore>());
        //context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
    }
}
