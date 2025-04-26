using System;
using System.Collections.Generic;
using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Domain.Entities;

namespace CSP_NET.Work.WorkManagement;

[Serializable]
public class CspWorkDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string WorkCode { get; private set; }
    public string TaskCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? StartProgressDate { get; set; }
    public DateTime? DueDate { get; set; }
    public float Progress { get; set; }
    public string? Duration { get; set; }
    public string Priority { get; set; }
    public string? Complexity { get; set; }
    public string? DegreeOfImportant { get; set; }
    public string Description { get; set; }
    public Guid? Owner { get; set; }
    public string? OwnerName { get; set; }
    public Guid? Assigner { get; set; }
    public string? AssignerName { get; set; }
    public Guid? Performer { get; set; }
    public string? PerformerName { get; set; }
    public string? Collaborators { get; set; }
    public string? CollaboratorNames { get; set; }
    public string? Members { get; set; }
    public bool RestrictComplete { get; set; }
    public CspWorkStatus Status { get; set; }
    public List<CspWorkCheckListDto>? CheckList { get; set; }
    public List<CspWorkNoteDto>? Notes { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    #region Tree
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public int Level { get; set; }
    public Guid? ParentId { get; set; }
    public string ConcurrencyStamp { get; set; }
    #endregion
}
