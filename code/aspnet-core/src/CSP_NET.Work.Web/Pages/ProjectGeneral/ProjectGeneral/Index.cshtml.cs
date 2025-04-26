using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CTIN.Csp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral;

public class IndexModel : WorkPageModel
{
    public ProjectGeneralFilterInput ProjectGeneralFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class ProjectGeneralFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectGeneralCode")]
    public string? Code { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectGeneralName")]
    public string? Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectGeneralDepartment")]
    public string? Department { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectGeneralState")]
    public string? State { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ProjectGeneralStatus")]
    public bool? Status { get; set; }
}
