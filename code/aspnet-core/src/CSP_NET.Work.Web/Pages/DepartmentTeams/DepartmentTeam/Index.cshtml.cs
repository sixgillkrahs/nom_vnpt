using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CTIN.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam;

public class IndexModel : WorkPageModel
{
    public DepartmentTeamFilterInput DepartmentTeamFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class DepartmentTeamFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamCode")]
    public string? Code { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamName")]
    public string? Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamDescription")]
    public string? Description { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamDepartmentID")]
    public Guid? DepartmentID { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamDepartment")]
    public OrganizationUnit? Department { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentTeamStatus")]
    public RecordStatusCode? Status { get; set; }

}
