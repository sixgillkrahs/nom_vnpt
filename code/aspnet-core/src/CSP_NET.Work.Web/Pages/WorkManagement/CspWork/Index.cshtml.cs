using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CTIN.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace CSP_NET.Work.Web.Pages.WorkManagement.CspWork;

public class IndexModel : WorkPageModel
{
    public CspWorkFilterInput CspWorkFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class CspWorkFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkWorkCode")]
    public bool? WorkCode { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkTaskCode")]
    public bool? TaskCode { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkStartDate")]
    public DateTime? StartDate { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkEstimate")]
    public DateTime? Estimate { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkDueDate")]
    public DateTime? DueDate { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkProgress")]
    public float? Progress { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkDuration")]
    public string? Duration { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkPriority")]
    public string? Priority { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkComplexity")]
    public string? Complexity { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkDegreeOfImportant")]
    public string? DegreeOfImportant { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkDescription")]
    public bool? Description { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkOwner")]
    public Guid? Owner { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkAssigner")]
    public Guid? Assigner { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkPerformer")]
    public Guid? Performer { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkcollaborators")]
    public string? collaborators { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkMembers")]
    public string? Members { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkCheckLists")]
    public ICollection<CheckList>? CheckLists { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkProjectId")]
    public Guid? ProjectId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkApproveId")]
    public Guid? ApproveId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkName")]
    public string? Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkFullName")]
    public string? FullName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkCode")]
    public string? Code { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkLevel")]
    public int? Level { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkParent")]
    public CspWork? Parent { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkParentId")]
    public Guid? ParentId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "CspWorkChildren")]
    public ICollection<CspWork>? Children { get; set; }
}
