using AutoMapper.Execution;
using CSP.Category.GeneralTreeManagement;
using CSP_NET.Work.Common;
using CSP_NET.Work.ConfigworkAccepts;
using CSP_NET.Work.LogWorks;
using CSP_NET.Work.ProjectGenerals;
using CSP_NET.Work.ResourseManagement;
using CTIN.Abp;
using CTIN.Abp.Domain.Entities.Auditing;
using CTIN.Abp.GeneralTree;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSP_NET.Work.WorkManagement;

public class CspWork : AuditedAggregateRoot<Guid>, IGeneralTree<CspWork, Guid>
{
    public string WorkCode { get; private set; }
    public string NomalizedWorkCode { get; set; }
    public string TaskCode { get; set; }
    public string NomalizedTaskCode { get; set; }
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
    public List<CspWorkCheckList>? CheckList { get; set; }
    public List<CspWorkNote>? Notes { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public ProjectGeneral ProjectGeneral { get; set; }
    public List<ConfigWorkAccept> ConfigWorkAccepts { get; set; }
    public ICollection<Documents> Documents { get; set; }
    public ICollection<LogWork> LogWorks { get; set; }
   

    #region Tree
    public string Name { get; set; }
    public string NomalizedName { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public int Level { get; set; }
    public CspWork Parent { get; set; }
    public Guid? ParentId { get; set; }
    public ICollection<CspWork> Children { get; set; }
    #endregion

    protected CspWork()

    {
    }

    public CspWork(
        Guid id,
        string workCode,
        string name,
        string taskCode,
        DateTime? startDate,
        DateTime? dueDate,
        float? progress,
        string? duration,
        string priority,
        string? complexity,
        string? degreeOfImportant,
        string description,
        Guid? owner,
        Guid? assigner,
        Guid? performer,
        string? collaborators,
        bool restrictComplete,
        Guid projectId,
        Guid? parentId
    ) : base(id)
    {
        SetWorkCode(workCode);
        SetName(name);
        SetTaskCode(taskCode);
        TaskCode = taskCode;
        StartDate = startDate;
        DueDate = dueDate;
        Progress = progress ?? 0;
        Duration = duration;
        Priority = priority;
        Complexity = complexity;
        DegreeOfImportant = degreeOfImportant;
        Description = description;
        Owner = owner;
        Assigner = assigner;
        Performer = performer;
        Collaborators = collaborators;
        SetMembers();
        RestrictComplete = restrictComplete;
        ProjectId = projectId;
        ParentId = parentId;
        Status = CspWorkStatus.New;
        CheckList = new();
        Notes = new();
    }

    private void SetName(string name)
    {
        Check.NotNull(name, nameof(name), CspWorkConsts.MaxNameLength);
        Name = name;
        NomalizedName = name.ToUpperInvariant();
    }

    internal CspWork ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
    private void SetWorkCode(string workCode)
    {
        Check.NotNull(workCode, nameof(workCode), CspWorkConsts.MaxCodeLength);
        WorkCode = workCode;
        NomalizedWorkCode = workCode.ToUpperInvariant();
    }

    internal CspWork ChangeWorkCode([NotNull] string code)
    {
        SetWorkCode(code);
        return this;
    }
        private void SetTaskCode(string taskCode)
    {
        Check.NotNull(taskCode, nameof(taskCode), CspWorkConsts.MaxCodeLength);
        TaskCode = taskCode;
        NomalizedTaskCode = taskCode.ToUpperInvariant();
    }

    internal CspWork ChangeTaskCode([NotNull] string taskCode)
    {
        SetTaskCode(taskCode);
        SetTaskCode(taskCode);
        return this;
    }
    internal CspWork SetMembers()
    {
        var setMem = new HashSet<string>();
        if (Owner.HasValue && Owner != null)
            setMem.Add(Owner.Value.ToString());
        if (Assigner.HasValue && Assigner != null)
            setMem.Add(Assigner.Value.ToString());
        if (Performer.HasValue && Performer != null)
            setMem.Add(Performer.Value.ToString());
        if (!Collaborators.IsNullOrEmpty())
        {
            var arrColl = Collaborators.Split(',');
            foreach (var mem in arrColl)
            {
                setMem.Add(mem);
            }
        }
        Members = string.Join(",", setMem);
        return this;
    }

}
