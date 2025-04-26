using System;
using System.ComponentModel.DataAnnotations;

namespace CSP_NET.Work.Web.Pages.WorkManagement.CspWork.ViewModels;

public class CreateEditCspWorkViewModel
{
    [Display(Name = "CspWorkWorkCode")]
    public bool WorkCode { get; set; }

    [Display(Name = "CspWorkTaskCode")]
    public bool TaskCode { get; set; }

    [Display(Name = "CspWorkStartDate")]
    public DateTime? StartDate { get; set; }

    [Display(Name = "CspWorkEstimate")]
    public DateTime? Estimate { get; set; }

    [Display(Name = "CspWorkDueDate")]
    public DateTime? DueDate { get; set; }

    [Display(Name = "CspWorkProgress")]
    public float? Progress { get; set; }

    [Display(Name = "CspWorkDuration")]
    public string? Duration { get; set; }

    [Display(Name = "CspWorkPriority")]
    public string? Priority { get; set; }

    [Display(Name = "CspWorkComplexity")]
    public string? Complexity { get; set; }

    [Display(Name = "CspWorkDegreeOfImportant")]
    public string? DegreeOfImportant { get; set; }

    [Display(Name = "CspWorkDescription")]
    public bool Description { get; set; }

    [Display(Name = "CspWorkOwner")]
    public Guid? Owner { get; set; }

    [Display(Name = "CspWorkAssigner")]
    public Guid? Assigner { get; set; }

    [Display(Name = "CspWorkPerformer")]
    public Guid? Performer { get; set; }

    [Display(Name = "CspWorkcollaborators")]
    public string? collaborators { get; set; }

    [Display(Name = "CspWorkMembers")]
    public string? Members { get; set; }

    [Display(Name = "CspWorkCheckLists")]
    public ICollection<CheckList> CheckLists { get; set; }

    [Display(Name = "CspWorkProjectId")]
    public Guid ProjectId { get; set; }

    [Display(Name = "CspWorkApproveId")]
    public Guid? ApproveId { get; set; }

    [Display(Name = "CspWorkName")]
    public string Name { get; set; }

    [Display(Name = "CspWorkFullName")]
    public string FullName { get; set; }

    [Display(Name = "CspWorkCode")]
    public string Code { get; set; }

    [Display(Name = "CspWorkLevel")]
    public int Level { get; set; }

    [Display(Name = "CspWorkParent")]
    public CspWork Parent { get; set; }

    [Display(Name = "CspWorkParentId")]
    public Guid? ParentId { get; set; }

    [Display(Name = "CspWorkChildren")]
    public ICollection<CspWork> Children { get; set; }
}
