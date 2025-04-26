using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using CTIN.Abp.Autofac;
using CTIN.Abp.Http.Client;
using CTIN.Abp.Http.Client.IdentityModel;
using CTIN.Abp.Modularity;

namespace CSP_NET.Work.HttpApi.Client.ConsoleTestApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WorkHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class WorkConsoleApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
            {
                clientBuilder.AddTransientHttpErrorPolicy(
                    policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                );
            });
        });
    }
}
