using CTIN.Abp.Modularity;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkApplicationModule),
    typeof(WorkDomainTestModule)
    )]
public class WorkApplicationTestModule : AbpModule
{

}
