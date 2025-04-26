using System;
using System.IO;
using System.Linq;
using Localization.Resources.AbpUi;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSP_NET.Work.EntityFrameworkCore;
using CSP_NET.Work.Localization;
using CSP_NET.Work.MultiTenancy;
using StackExchange.Redis;
using CTIN.Abp;
using CTIN.Abp.Account;
using CTIN.Abp.Account.Web;
using CTIN.Abp.AspNetCore.Mvc.UI;
using CTIN.Abp.AspNetCore.Mvc.UI.Bootstrap;
using CTIN.Abp.AspNetCore.Mvc.UI.Bundling;
using CTIN.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using CTIN.Abp.AspNetCore.Serilog;
using CTIN.Abp.Auditing;
using CTIN.Abp.Autofac;
using CTIN.Abp.BackgroundJobs;
using CTIN.Abp.Caching;
using CTIN.Abp.Caching.StackExchangeRedis;
using CTIN.Abp.DistributedLocking;
using CTIN.Abp.Localization;
using CTIN.Abp.Modularity;
using CTIN.Abp.UI.Navigation.Urls;
using CTIN.Abp.UI;
using CTIN.Abp.VirtualFileSystem;
using CTIN.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using CTIN.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CSP_NET.Work;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),
    //typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(WorkEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
public class WorkAuthServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Work");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WorkResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });

        Configure<AbpAuditingOptions>(options =>
        {
                //options.IsEnabledForGetRequests = true;
                options.ApplicationName = "AuthServer";
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<WorkDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<WorkDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Domain"));
            });
        }

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });

        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Work:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Work");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Work-Protection-Keys");
        }

        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer
                .Connect(configuration["Redis:Configuration"]);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        context.Services.AddAuthentication().AddOpenIdConnect("Ids", options =>
        {
            options.CallbackPath = configuration["IdsOidc:CallbackPath"];
            options.AuthenticationMethod = Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectRedirectBehavior.RedirectGet;
            options.Authority = configuration["IdsOidc:Authority"];
            options.ClientSecret = configuration["IdsOidc:ClientSecret"];
            options.ClientId = configuration["IdsOidc:ClientId"];
            options.ResponseType = OpenIdConnectResponseType.Code;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
