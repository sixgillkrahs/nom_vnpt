using CSP_NET.Work.WorkManagement;
using CSP_NET.Work.ProjectState;
using CSP_NET.Work.DepartmentTeams;
using CSP_NET.Work.ProjectGenerals;
using System;
using Microsoft.Extensions.DependencyInjection;
using CTIN.Abp.Uow;
using CTIN.Abp.AuditLogging.EntityFrameworkCore;
using CTIN.Abp.BackgroundJobs.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore;
using CTIN.Abp.EntityFrameworkCore.SqlServer;
using CTIN.Abp.FeatureManagement.EntityFrameworkCore;
using CTIN.Abp.Identity.EntityFrameworkCore;
using CTIN.Abp.Modularity;
using CTIN.Abp.OpenIddict.EntityFrameworkCore;
using CTIN.Abp.PermissionManagement.EntityFrameworkCore;
using CTIN.Abp.SettingManagement.EntityFrameworkCore;
using CTIN.Abp.TenantManagement.EntityFrameworkCore;
using CTIN.Abp.NotificationService.EntityFrameworkCore;
using CSP.Category.EntityFrameworkCore;
using CSP.NotificationConfig.EntityFrameworkCore;

namespace CSP_NET.Work.EntityFrameworkCore;

[DependsOn(
    typeof(WorkDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(NotificationServiceEntityFrameworkCoreModule),
    typeof(CategoryEntityFrameworkCoreModule)
    )]
[DependsOn(typeof(NotificationConfigEntityFrameworkCoreModule))]
public class WorkEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        WorkEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WorkDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<ProjectGenerals.ProjectGeneral, ProjectGeneralRepository>();
            options.AddRepository<DepartmentTeam, DepartmentTeamRepository>();
            options.AddRepository<ProjectState.ProjectState, ProjectStateRepository>();
            options.AddRepository<CspWork, CspWorkRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also WorkMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
