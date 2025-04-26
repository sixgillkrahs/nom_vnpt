using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CTIN.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace CSP_NET.Work.Web.Pages.ProjectState.ProjectState;

public class IndexModel : WorkPageModel
{
    public ProjectStateFilterInput ProjectStateFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class ProjectStateFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectStateCode")]
    public string? Code { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectStateName")]
    public string? Name { get; set; }
}
