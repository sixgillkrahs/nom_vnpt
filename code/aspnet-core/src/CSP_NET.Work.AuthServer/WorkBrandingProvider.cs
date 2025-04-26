using CTIN.Abp.Ui.Branding;
using CTIN.Abp.DependencyInjection;

namespace CSP_NET.Work;

[Dependency(ReplaceServices = true)]
public class WorkBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Work";
}
