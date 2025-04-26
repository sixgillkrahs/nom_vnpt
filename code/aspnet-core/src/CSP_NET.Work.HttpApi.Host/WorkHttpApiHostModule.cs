using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSP_NET.Work.EntityFrameworkCore;
using CSP_NET.Work.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using CTIN.Abp;
using CTIN.Abp.AspNetCore.Mvc;
using CTIN.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using CTIN.Abp.AspNetCore.Serilog;
using CTIN.Abp.Autofac;
using CTIN.Abp.Caching;
using CTIN.Abp.Caching.StackExchangeRedis;
using CTIN.Abp.DistributedLocking;
using CTIN.Abp.Localization;
using CTIN.Abp.Modularity;
using CTIN.Abp.Swashbuckle;
using CTIN.Abp.VirtualFileSystem;
using CSP_NET.Work.Menu;
using CTIN.Abp.BlobStoring.Minio;
using CTIN.Abp.BlobStoring;
using Microsoft.AspNetCore.Identity;
using CSP_NET.Work.SyncCtin;
using System.Net.Http.Headers;
using System.Net.Http;
using CTIN.Abp.BackgroundWorkers.Hangfire;
using CTIN.Abp.BackgroundWorkers;
using Hangfire;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(WorkApplicationModule),
    typeof(WorkEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBlobStoringMinioModule),
    typeof(AbpBackgroundWorkersHangfireModule),
    typeof(AbpBackgroundWorkersModule)
)]
public class WorkHttpApiHostModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IdentityBuilder>(builder =>
        {
            builder.AddDefaultTokenProviders();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureConventionalControllers();
        ConfigureAuthentication(context, configuration);
        ConfigureCache(configuration);
        ConfigureVirtualFileSystem(context);
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureDistributedLocking(context, configuration);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
        ConfigureBlob(context, configuration);

        Configure<MenuOptions>(options => { options.MultiTenant = false; });

        ConfigureCTINHttpClient(context, configuration);
        ConfigureHangfire(context, configuration);
    }

    private static void ConfigureCTINHttpClient(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHttpContextAccessor();

        context.Services.AddHttpClient(SyncCtinConsts.IDS, (serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration["CtinSync:IDS:url"] + "");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var access_token = await accessor.HttpContext.GetTokenAsync("access_token");
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["CtinSync:IDS:token"] + "");
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
        }).UseExponentialHttpRetryPolicy();

        context.Services.AddHttpClient(SyncCtinConsts.HRM, (serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration["CtinSync:HRM:url"] + "");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var access_token = await accessor.HttpContext.GetTokenAsync("access_token");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["CtinSync:HRM:token"] + "");
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
        }).UseExponentialHttpRetryPolicy();

        context.Services.AddHttpClient(SyncCtinConsts.PMS, (serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration["CtinSync:PMS:url"] + "");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var access_token = await accessor.HttpContext.GetTokenAsync("access_token");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["CtinSync:PMS:token"] + "");
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
        }).UseExponentialHttpRetryPolicy();


        context.Services.AddHttpClient(SyncCtinConsts.IDSUAT, (serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration["CtinSync:IDSUAT:url"] + "");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //var access_token = await accessor.HttpContext.GetTokenAsync("access_token");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["CtinSync:IDSUAT:token"] + "");
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
        }).UseExponentialHttpRetryPolicy();
    }
    private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("Default"));
        });
    }

    private void ConfigureBlob(ServiceConfigurationContext context, IConfiguration configuration)
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseMinio(minio =>
                {
                    minio.EndPoint = configuration["minio:EndPoint"];
                    
                    minio.AccessKey = configuration["minio:AccessKey"];
                    minio.SecretKey = configuration["minio:SecretKey"];
                    minio.BucketName = configuration["minio:BucketName"];
                });
            });
        });
    }


    private void ConfigureCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Work:"; });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<WorkDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<WorkDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<WorkApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<WorkApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}CSP_NET.Work.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(WorkApplicationModule).Assembly);
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "Work";
            });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                    {"Work", "Work API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Work API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private void ConfigureDataProtection(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Work");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Work-Protection-Keys");
        }
    }

    private void ConfigureDistributedLocking(
        ServiceConfigurationContext context,
        IConfiguration configuration)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer
                .Connect(configuration["Redis:Configuration"]);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? Array.Empty<string>())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
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
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Work API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("Work");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseHangfireServer(new BackgroundJobServerOptions() { Queues = new string[] { WorkConsts.BackgroundWorkerQueue } });
        app.UseHangfireDashboard();
        app.UseConfiguredEndpoints();
        context.AddBackgroundWorkerAsync<SyncCtinDeparmentWorker>().Wait();
        context.AddBackgroundWorkerAsync<SyncCtinUserWorker>().Wait();
        //context.AddBackgroundWorkerAsync<SyncCtinTenantWorker>().Wait();
    }
}
