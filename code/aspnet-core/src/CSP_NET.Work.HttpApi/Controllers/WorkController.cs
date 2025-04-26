using CSP_NET.Work.Localization;
using CTIN.Abp.AspNetCore.Mvc;

namespace CSP_NET.Work.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WorkController : AbpControllerBase
{
    protected WorkController()
    {
        LocalizationResource = typeof(WorkResource);
    }
}
