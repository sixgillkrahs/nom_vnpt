using CSP_NET.Work.Common;
using CTIN.Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSP_NET.Work.WorkManagement;

[Serializable]
public class CreateUpdateCspWorkDto 
{
    [Required]
    [MaxLength(CspWorkConsts.MaxCodeLength)]
    public string WorkCode {
        get { return WorkCode.Trim(); }
        set { WorkCode = value.Trim(); } 
    }
    [Required]
    [MaxLength(CspWorkConsts.MaxNameLength)]
    public string Name
    {
        get { return Name.Trim(); }
        set { Name = value.Trim(); }
    }
    [Required]
    [MaxLength(CspWorkConsts.MaxCodeLength)]
    public string TaskCode
    {
        get { return TaskCode.Trim(); }
        set { TaskCode = value.Trim(); }
    }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public float? Progress { get; set; }
    [MaxLength(CspWorkConsts.MaxDurationLength)]
    [RegularExpression(RegexDefine.Duration)]
    public string? Duration { get; set; }
    [Required]
    [MaxLength(CspWorkConsts.MaxPriorityLength)]
    public string Priority { get; set; }
    [MaxLength(CspWorkConsts.MaxComplextityLength)]
    public string? Complexity { get; set; }
    [MaxLength(CspWorkConsts.MaxDegreeOfImportantLength)]
    public string? DegreeOfImportant { get; set; }
    [Required]
    [MaxLength(CspWorkConsts.MaxDescriptionLength)]
    public string Description { get; set; }
    public Guid? Owner { get; set; }
    public Guid? Assigner { get; set; }
    public Guid? Performer { get; set; }
    public string? Collaborators { get; set; }
    public List<CspWorkCheckListDto>? CheckList { get; set; }
    [Required]
    public Guid ProjectId { get; set; }
    public bool RestrictComplete { get; set; }
}
