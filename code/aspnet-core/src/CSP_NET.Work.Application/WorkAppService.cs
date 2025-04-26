using System;
using System.Collections.Generic;
using System.Text;
using CSP_NET.Work.Localization;
using CTIN.Abp.Application.Services;

namespace CSP_NET.Work;

/* Inherit your application services from this class.
 */
public abstract class WorkAppService : ApplicationService
{
    protected WorkAppService()
    {
        LocalizationResource = typeof(WorkResource);
    }
}
