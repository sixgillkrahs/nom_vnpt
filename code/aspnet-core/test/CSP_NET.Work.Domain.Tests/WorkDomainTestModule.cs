using CSP_NET.Work.EntityFrameworkCore;
using CTIN.Abp.Modularity;

namespace CSP_NET.Work;

[DependsOn(
    typeof(WorkEntityFrameworkCoreTestModule)
    )]
public class WorkDomainTestModule : AbpModule
{

}
