using Microsoft.EntityFrameworkCore;
using CTIN.Abp.AuditLogging.EntityFrameworkCore;
using CTIN.Abp.BackgroundJobs.EntityFrameworkCore;
using CTIN.Abp.Data;
using CTIN.Abp.DependencyInjection;
using CTIN.Abp.EntityFrameworkCore;
using CTIN.Abp.FeatureManagement.EntityFrameworkCore;
using CTIN.Abp.Identity;
using CTIN.Abp.Identity.EntityFrameworkCore;
using CTIN.Abp.OpenIddict.EntityFrameworkCore;
using CTIN.Abp.PermissionManagement.EntityFrameworkCore;
using CTIN.Abp.SettingManagement.EntityFrameworkCore;
using CTIN.Abp.TenantManagement;
using CTIN.Abp.TenantManagement.EntityFrameworkCore;
using CTIN.Abp.NotificationService.EntityFrameworkCore;
using CTIN.Abp.NotificationService.NotificationInfos;
using CSP.Category.EntityFrameworkCore;
using CSP.Category.DefineVersionManagement;
using CSP.Category.CategoryManagement;
using CTIN.Abp.EntityFrameworkCore.Modeling;
using CSP.Category.GeneralTreeManagement;
using CSP.NotificationConfig.EntityFrameworkCore;
using CSP_NET.Work.DepartmentTeams;
using CSP.NotificationConfig.MailSetting;
using CSP.NotificationConfig.TextTemplate;
using CSP.NotificationConfig.NotificationFactory;
using CSP_NET.Work.Common;
using CSP_NET.Work.ConfigworkAccepts;
using CSP_NET.Work.WorkManagement;
using CTIN.Abp.Domain.Entities;
//using Volo.Abp.EntityFrameworkCore.Modeling;
using CSP_NET.Work.LogWorks;
using CSP_NET.Work.ResourseManagement;
using System.Reflection.Metadata;

namespace CSP_NET.Work.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ReplaceDbContext(typeof(INotificationServiceDbContext))]
[ReplaceDbContext(typeof(ICategoryDbContext))]
[ReplaceDbContext(typeof(INotificationConfigDbContext))]
[ConnectionStringName("Default")]
public class WorkDbContext :
    AbpDbContext<WorkDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext,
    INotificationServiceDbContext,
    ICategoryDbContext,
    INotificationConfigDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }


    public DbSet<Menu.Menu> Menus { get; set; }
    public DbSet<DefineVersion> DefineVersions { get; set; }
    public DbSet<CSPCategory> Categories { get; set; }
    public DbSet<CTIN.Abp.NotificationService.Notifications.Notification> Notifications { get; set; }
    public DbSet<NotificationInfo> NotificationInfos { get; set; }

    public DbSet<OrganizationUnitType> OrganizationUnitTypes { get; set; }

    public DbSet<ProjectRole.ProjectRole> ProjectRoles { get; set; }

    #endregion
    public DbSet<ProjectGenerals.ProjectGeneral> ProjectGenerals { get; set; }

    public DbSet<ProjectState.ProjectState> ProjectStates { get; set; }
    public DbSet<CspGeneralTree> GeneralTrees { get; set; }
    public DbSet<DepartmentTeam> DepartmentTeams { get; set; }
    public DbSet<MailSetting> MailSettings { get; set; }
    public DbSet<TextTemplate> TextTemplates { get; set; }
    public DbSet<NotificationFactory> NotificationFactories { get; set; }
    public DbSet<CspWork> CspWorks { get; set; }
    public DbSet<ConfigWorkAccept> ConfigworkAccepts { get; set; }
    public DbSet<LogWork> LogWorks { get; set; }
    public DbSet<Documents> Documents { get; set; }
    public WorkDbContext(DbContextOptions<WorkDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ConfigureCategory();
        builder.ConfigureNotificationService();
        builder.ConfigureNotificationConfig();

        /* Configure your own tables/entities inside here */

        builder.Entity<ProjectRole.ProjectRole>(entity =>
        {
            entity.HasKey(x => x.Id).HasName("SYS_C435324523500");
            entity.ToTable(WorkConsts.DbTablePrefix + "PROJECT_ROLES", WorkConsts.DbSchema);

            entity.Property(x => x.Name).HasColumnName("NAME");
            entity.Property(x => x.Code).HasColumnName("CODE");
            entity.Property(x => x.Domain).HasColumnName("DOMAIN");
            //entity.Property(x => x.Status).HasColumnName("STATUS");
        });

        builder.Entity<ProjectState.ProjectState>(entity =>
        {
            entity.HasKey(x => x.Id).HasName("SYS_C435324523509");
            entity.ToTable(WorkConsts.DbTablePrefix + "PROJECT_STATES", WorkConsts.DbSchema);

            entity.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(ProjectStateConsts.MaxNameLength);
            entity.Property(x => x.Code).HasColumnName("CODE").HasMaxLength(ProjectStateConsts.MaxCodeLength);

        });


        builder.Entity<ProjectGenerals.ProjectGeneral>(entity =>
        {
            entity.HasKey(x => x.Id).HasName("SYS_C435324523501");
            entity.ToTable(WorkConsts.DbTablePrefix + "PROJECT_GENERAL", WorkConsts.DbSchema);
            entity.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(ProjectGeneralConsts.MaxNameLength);
            entity.Property(x => x.Code).HasColumnName("CODE").HasMaxLength(ProjectGeneralConsts.MaxCodeLength);
            entity.Property(x => x.DepartmentID).HasColumnName("DEPARTMENTID");
            entity.Property(x => x.Description).HasColumnName("DESCRIPTION").HasMaxLength(ProjectGeneralConsts.MaxDescriptionLength);
            entity.Property(x => x.ProjectStateID).HasColumnName("PROJECTSTATEID");
            entity.Property(x => x.Status).HasColumnName("STATUS");
            entity.ConfigureByConvention();


            /* Configure more properties here */
        });

        builder.Entity<DepartmentTeamMember>(entity =>
        {
            entity.HasKey(sc => new { sc.DepartmentTeamId, sc.UserId }).HasName("SYS_C435324523505");
            entity.ToTable(WorkConsts.DbTablePrefix + "DEPARTMENT_TEAM_MEMBER", WorkConsts.DbSchema);
            entity.HasOne<DepartmentTeam>().WithMany(p => p.Members).HasForeignKey(c => c.DepartmentTeamId).HasConstraintName("FK_DEPARTMENT_TEAM_MEMBER");
            entity.HasOne(sc => sc.Member)
            .WithMany().HasForeignKey(sc => sc.UserId).HasConstraintName("FK_DEPARTMENT_TEAM_USER");
        });

        builder.Entity<DepartmentTeam>(entity =>
        {
            entity.HasKey(x => x.Id).HasName("SYS_C435324523502");
            entity.ToTable(WorkConsts.DbTablePrefix + "DEPARTMENT_TEAMS", WorkConsts.DbSchema);

            entity.Property(x => x.Name).HasColumnName("NAME");
            entity.Property(x => x.Code).HasColumnName("CODE");
            entity.Property(x => x.Description).HasColumnName("DESCRIPTION");
            entity.Property(x => x.DepartmentID).HasColumnName("DEPARTMENT_ID");
            entity.Property(x => x.Status).HasColumnName("STATUS");

            entity.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentID).HasConstraintName("FK_DEPARTMENT_TEAM_ORGANIZATION");

            entity.ConfigureByConvention();


            /* Configure more properties here */
        });

        builder.Entity<ConfigWorkAccept>(b =>
        {
            b.HasKey(x => x.Id).HasName("SYS_C435324523809");
            b.ToTable(WorkConsts.DbTablePrefix + "CONFIGWORK_ACCEPTS", WorkConsts.DbSchema);
            b.Property(x => x.WorkId).HasColumnName("WORK_ID");
            b.Property(x => x.UserAccept1).HasColumnName("USERACCEPT1");
            b.Property(x => x.DateAccept1).HasColumnName("DATEACCEPT1");
            b.Property(x => x.UserAccept2).HasColumnName("USERACCEP2");
            b.Property(x => x.DateAccept2).HasColumnName("DATEACCEPT2");
            b.Property(x => x.StatusAccept1).HasColumnName("STATUSACCEPT1");
            b.Property(x => x.StatusAccept2).HasColumnName("STATUSACCEPT2");
            b.Property(x => x.Accept2).HasColumnName("ACCEPT2");


            b.HasOne(x => x.CspWork)
              .WithMany(p => p.ConfigWorkAccepts)
              .HasForeignKey(x => x.WorkId)
              .IsRequired(false);

            b.ConfigureByConvention();
        });


        builder.Entity<CspWork>(b =>
        {
            //b.HasKey(x => x.Id).HasName("SYS_C435324523810");
            b.ToTable(WorkConsts.DbTablePrefix + "CspWorks", WorkConsts.DbSchema);

            b.Property(x => x.WorkCode).HasColumnName("WORK_CODE").HasMaxLength(CspWorkConsts.MaxCodeLength);
            b.Property(x => x.NomalizedWorkCode).HasColumnName("NOMALIZED_WORK_CODE").HasMaxLength(CspWorkConsts.MaxCodeLength);
            b.Property(x => x.TaskCode).HasColumnName("TASK_CODE").HasMaxLength(CspWorkConsts.MaxCodeLength);
            b.Property(x => x.NomalizedTaskCode).HasColumnName("NOMALIZED_TASK_CODE").HasMaxLength(CspWorkConsts.MaxCodeLength);
            b.Property(x => x.StartDate).HasColumnName("START_DATE");
            b.Property(x => x.StartProgressDate).HasColumnName("START_PROGRESS_DATE");
            b.Property(x => x.DueDate).HasColumnName("DUE_DATE");
            b.Property(x => x.Progress).HasColumnName("PROGRESS").HasDefaultValue(0);
            b.Property(x => x.Duration).HasColumnName("DURATION").HasMaxLength(CspWorkConsts.MaxDurationLength);
            b.Property(x => x.Priority).HasColumnName("PRIORITY").HasMaxLength(CspWorkConsts.MaxPriorityLength);
            b.Property(x => x.Complexity).HasColumnName("COMPLEXITY").HasMaxLength(CspWorkConsts.MaxComplextityLength);
            b.Property(x => x.DegreeOfImportant).HasColumnName("DEGREE_OF_IMPORTANT").HasMaxLength(CspWorkConsts.MaxDegreeOfImportantLength);
            b.Property(x => x.Description).HasColumnName("DESCRIPTION").HasMaxLength(CspWorkConsts.MaxDescriptionLength);
            b.Property(x => x.Owner).HasColumnName("OWNER");
            b.Property(x => x.OwnerName).HasColumnName("OWNER_Name").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.Assigner).HasColumnName("ASSIGNER");
            b.Property(x => x.AssignerName).HasColumnName("ASSIGNER_NAME").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.Performer).HasColumnName("PERFORMER"); ;
            b.Property(x => x.PerformerName).HasColumnName("PERFORMER_NAME").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.Collaborators).HasColumnName("COLLABORATORS");
            b.Property(x => x.CollaboratorNames).HasColumnName("COLLABORATOR_NAMES");
            b.Property(x => x.Members).HasColumnName("MEMBERS");
            b.Property(x => x.Status).HasColumnName("STATUS").HasDefaultValue(CspWorkStatus.New);
            b.Property(x => x.ProjectName).HasColumnName("PROJECT_NAME").HasMaxLength(ProjectGeneralConsts.MaxNameLength);

            b.OwnsMany(
              w => w.CheckList, ownedNavigationBuilder =>
              {
                  ownedNavigationBuilder.ToJson();
              });
            b.OwnsMany(
              w => w.Notes, ownedNavigationBuilder =>
              {
                  ownedNavigationBuilder.ToJson();
              });

            b.Property(x => x.ProjectId).HasColumnName("PROJECT_ID");
            b.Property(x => x.RestrictComplete).HasColumnName("RESTRICT_COMPLETE").HasDefaultValue(false);

            b.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.NomalizedName).HasColumnName("NOMALIZED_NAME").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.Code).HasColumnName("CODE");
            b.Property(x => x.FullName).HasColumnName("FULL_NAME");
            b.Property(x => x.Level).HasColumnName("LEVEL").HasDefaultValue(0);
            b.Property(x => x.ParentId).HasColumnName("PARENT_ID");

            b.HasIndex(x => x.Code);
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.NomalizedName);
            b.HasIndex(x => x.NomalizedTaskCode);
            b.HasIndex(x => x.NomalizedWorkCode);
            b.HasIndex(x => x.WorkCode);
            b.HasIndex(x => x.TaskCode);
            b.HasIndex(x => x.Priority);
            b.HasIndex(x => x.Status);

            b.HasOne(x => x.ProjectGeneral)
             .WithMany(p => p.CspWorks)
             .HasForeignKey(x => x.ProjectId)
             .IsRequired(false);

            b.HasOne(x => x.Parent)
             .WithMany(x => x.Children)
             .HasForeignKey(x => x.ParentId)
             .IsRequired(false);

            b.ConfigureByConvention();
        });

        builder.Entity<LogWork>(b =>
        {
            b.HasKey(x => x.Id).HasName("SYS_C435324523811");
            b.ToTable(WorkConsts.DbTablePrefix + "LogWork", WorkConsts.DbSchema);
            b.Property(x => x.WorkId).HasColumnName("WORKID");
            b.Property(x => x.UserId).HasColumnName("USERID");
            b.Property(x => x.Time).HasColumnName("TIME");
            b.Property(x => x.Date).HasColumnName("DATE");
            b.Property(x => x.Description).HasColumnName("Description");
            b.Property(x => x.StatusLogWork).HasColumnName("StatusLogWork");

            b.HasOne(x => x.CspWork)
             .WithMany(p => p.LogWorks)
             .HasForeignKey(x => x.WorkId)
             .IsRequired(false);

            b.ConfigureByConvention();
        });
      
        builder.Entity<Documents>(b =>
        {
            b.HasKey(x => x.Id).HasName("SYS_C435324523812");
            b.ToTable(WorkConsts.DbTablePrefix + "Document", WorkConsts.DbSchema);
            b.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(CspWorkConsts.MaxNameLength);
            b.Property(x => x.WorkId).HasColumnName("WORKID");
            b.Property(x => x.UpdateDate).HasColumnName("UPDATEDATE");
            b.Property(x => x.UpdateBy).HasColumnName("UPDATEBY");
            b.Property(x => x.MimeType).HasColumnName("MIMETYPE");
            b.Property(x => x.FileSize).HasColumnName("FILESIZE");
            b.HasOne(x => x.CspWork)
            .WithMany(p => p.Documents)
            .HasForeignKey(x => x.WorkId)
            .IsRequired(false);


            b.ConfigureByConvention();
        });
    }
}
