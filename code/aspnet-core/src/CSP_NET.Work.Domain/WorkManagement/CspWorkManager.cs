using CSP.Category.GeneralTreeManagement;
using CSP_NET.Work.ProjectGenerals;
using CTIN.Abp;
using CTIN.Abp.Domain.Services;
using CTIN.Abp.GeneralTree;
using CTIN.Abp.Guids;
using CTIN.Abp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CTIN.Abp.Identity.Settings.IdentitySettingNames;

namespace CSP_NET.Work.WorkManagement;

public class CspWorkManager : DomainService
{
    private readonly IGeneralTreeManager<CspWork, Guid> _generalTreeManager;
    private readonly ICspWorkRepository _repository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IProjectGeneralRepository _projectGeneralrepository;

    public CspWorkManager(IGeneralTreeManager<CspWork, Guid> generalTreeManager,
        ICspWorkRepository repository,
        IGuidGenerator guidGenerator,
        IIdentityUserRepository identityUserRepository,
        IProjectGeneralRepository projectGeneralrepository)
    {
        _generalTreeManager = generalTreeManager;
        _repository = repository;
        _guidGenerator = guidGenerator;
        _identityUserRepository = identityUserRepository;
        _projectGeneralrepository = projectGeneralrepository;
    }
    public async Task<CspWork> CreateAsync(
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
        List<CspWorkCheckList>? checkList,
        bool restrictComplete,
        Guid projectId,
        Guid? parentId
        )
    {
        var existedEntity = await _repository.FindAsync(x => x.WorkCode == workCode || x.Name == name);
        if (existedEntity != null)
        {
            if (existedEntity.WorkCode == workCode)
            {
                throw new BusinessException(WorkDomainErrorCodes.AlreadyExistWorkCode)
                    .WithData("code", workCode);
            }
            if (existedEntity.Name == name)
            {
                throw new BusinessException(WorkDomainErrorCodes.AlreadyExistWorkName)
                    .WithData("name", name);
            }
        }
        var cspWork = new CspWork(
            _guidGenerator.Create(),
            workCode,
            name,
            taskCode,
            startDate,
            dueDate,
            progress,
            duration,
            priority,
            complexity,
            degreeOfImportant,
            description,
            owner,
            assigner,
            performer,
            collaborators,
            restrictComplete,
            projectId,
            parentId
            )
        {
            CheckList = checkList
        };
        var project = await _projectGeneralrepository.GetAsync(cspWork.ProjectId);
        cspWork.ProjectName = project.Name;

        var userIds = new List<Guid>();
        if (owner.HasValue) userIds.Add(owner.Value);
        if (assigner.HasValue) userIds.Add(assigner.Value);
        if (performer.HasValue) userIds.Add(performer.Value);
        if (!collaborators.IsNullOrEmpty())
        {
            var colls = collaborators.Split(',');
            for (int i = 0; i < colls.Length; i++)
            {
                userIds.Add(new Guid(colls[i]));
            }
        }
        if (userIds.Count > 0)
        {
            var users = await _identityUserRepository.GetListByIdsAsync(userIds);
            if (owner.HasValue)
            {
                var user = users.Find(x => x.Id == owner.Value);
                cspWork.OwnerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (assigner.HasValue)
            {
                var user = users.Find(x => x.Id == assigner.Value);
                cspWork.AssignerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (performer.HasValue)
            {
                var user = users.Find(x => x.Id == performer.Value);
                cspWork.PerformerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (!collaborators.IsNullOrEmpty())
            {
                var cools = users
                           .Where(x => collaborators.Contains(x.Id.ToString()))
                           .Select(u => $"{u.Surname} {u.Name} - {u.UserName}")
                           .ToList();
                cspWork.CollaboratorNames = string.Join(",", cools);
            }
        }
        cspWork.SetMembers();
        var entity = await _generalTreeManager.CreateAsync(cspWork, true);
        cspWork.Code = entity.Code;
        cspWork.FullName = entity.FullName;
        cspWork.Level = entity.Level;
        return cspWork;
    }
    public async Task<CspWork> UpdateAsync(
       CspWork cspWork,
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
       List<CspWorkCheckList>? checkList,
       List<CspWorkNote>? notes,
       bool restrictComplete,
       Guid projectId
       //Guid? parentId
       )
    {
        if (cspWork.WorkCode != workCode)
        {
            var existedEntity = await _repository.FindAsync(x => x.WorkCode == workCode);
            if (existedEntity != null)
                throw new BusinessException(WorkDomainErrorCodes.AlreadyExistWorkCode)
                    .WithData("code", workCode);
            cspWork.ChangeWorkCode(workCode);
        }
        if (cspWork.Name != name)
        {
            var existedEntity = await _repository.FindAsync(x => x.Name == name);
            if (existedEntity != null)
                throw new BusinessException(WorkDomainErrorCodes.AlreadyExistWorkName)
                .WithData("name", name);
            cspWork.ChangeName(name);
        }
        if (cspWork.TaskCode != taskCode)
        {
            cspWork.ChangeTaskCode(taskCode);
        }
        //if (!cspWork.ParentId.Equals(parentId))
        //{
        //    await MoveAsync(cspWork.Id, parentId);
        //}
        cspWork.StartDate = startDate;
        cspWork.DueDate = dueDate;
        cspWork.Progress = progress ?? 0;
        cspWork.Duration = duration;
        cspWork.Priority = priority;
        cspWork.Complexity = complexity;
        cspWork.DegreeOfImportant = degreeOfImportant;
        cspWork.Description = description;
        cspWork.Owner = owner;
        cspWork.Assigner = assigner;
        cspWork.Performer = performer;
        cspWork.Collaborators = collaborators;
        cspWork.CheckList = checkList;
        cspWork.Notes = notes;
        cspWork.RestrictComplete = restrictComplete;
        if(cspWork.ProjectId != projectId)
        {
            cspWork.ProjectId = projectId;
            var project = await _projectGeneralrepository.GetAsync(cspWork.ProjectId);
            cspWork.ProjectName = project.Name;
        }

        var userIds = new List<Guid>();
        if (owner.HasValue) userIds.Add(owner.Value);
        if (assigner.HasValue) userIds.Add(assigner.Value);
        if (performer.HasValue) userIds.Add(performer.Value);
        if (!collaborators.IsNullOrEmpty())
        {
            var colls = collaborators.Split(',');
            for (int i = 0; i < colls.Length; i++)
            {
                userIds.Add(new Guid(colls[i]));
            }
        }
        if (userIds.Count > 0)
        {
            var users = await _identityUserRepository.GetListByIdsAsync(userIds);
            if (owner.HasValue)
            {
                var user = users.Find(x => x.Id == owner.Value);
                cspWork.OwnerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (assigner.HasValue)
            {
                var user = users.Find(x => x.Id == assigner.Value);
                cspWork.AssignerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (performer.HasValue)
            {
                var user = users.Find(x => x.Id == performer.Value);
                cspWork.PerformerName = $"{user.Surname} {user.Name} - {user.UserName}";
            }
            if (!collaborators.IsNullOrEmpty())
            {
                var cools = users
                           .Where(x => collaborators.Contains(x.Id.ToString()))
                           .Select(u => $"{u.Surname} {u.Name} - {u.UserName}")
                           .ToList();
                cspWork.CollaboratorNames = string.Join(",", cools);
            }
        }
        cspWork.SetMembers();
        await _generalTreeManager.UpdateAsync(cspWork);
        return cspWork;
    }
    public async Task DeleteAsync(Guid id)
    {
        await _generalTreeManager.DeleteAsync(id);
    }
    public async Task MoveAsync(Guid id, Guid? parentId)
    {
        await _generalTreeManager.MoveAsync(id, parentId);
    }
}
