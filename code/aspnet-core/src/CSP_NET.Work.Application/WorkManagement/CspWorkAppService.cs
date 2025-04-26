using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using CSP.Category.GeneralTreeManagement;
using CSP_NET.Work.Common;
using CSP_NET.Work.Permissions;
using CTIN.Abp;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.Identity;
using CTIN.Abp.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSP_NET.Work.WorkManagement;


public class CspWorkAppService :
    CrudAppService<CspWork,
        CspWorkDto, Guid,
        CspWorkGetListInput,
        CreateCspWorkDto,
        UpdateCspWorkDto>,
    ICspWorkAppService
{

    private readonly ICspWorkRepository _repository;
    private readonly CspWorkManager _manager;

    public CspWorkAppService(ICspWorkRepository repository, CspWorkManager manager) : base(repository)
    {
        _repository = repository;
        _manager = manager;
    }

    [Authorize]
    public override async Task<CspWorkDto> CreateAsync(CreateCspWorkDto input)
    {
        var checkList = input.CheckList != null ? ObjectMapper.Map<List<CspWorkCheckListDto>, List<CspWorkCheckList>>(input.CheckList) : null;

        var cspWork = await _manager.CreateAsync(
            workCode: input.WorkCode.Trim(),
            name: input.Name.Trim(),
            taskCode: input.TaskCode.Trim(),
            startDate: input.StartDate,
            dueDate: input.DueDate,
            progress: input.Progress,
            duration: input.Duration,
            priority: input.Priority,
            complexity: input.Complexity,
            degreeOfImportant: input.DegreeOfImportant,
            description: input.Description,
            owner: input.Owner,
            assigner: input.Assigner,
            performer: input.Performer,
            collaborators: input.Collaborators,
            checkList: checkList,
            restrictComplete: input.RestrictComplete,
            projectId: input.ProjectId,
            parentId: input.ParentId
            );
        return ObjectMapper.Map<CspWork, CspWorkDto>(cspWork);
    }

    [Authorize]
    public override async Task<CspWorkDto> UpdateAsync(Guid id, UpdateCspWorkDto input)
    {
        var oldCspWork = await _repository.GetAsync(id);
        if (CurrentUser.Id != oldCspWork.Owner
            && CurrentUser.Id != oldCspWork.Assigner
            && CurrentUser.Id != oldCspWork.Performer
            && !CurrentUser.Roles.Any(x => x == "admin"))
        {
            throw new BusinessException("AbpIdentity::DefaultErrorMessage403Detail");
        }
        input.SetConcurrencyStampIfNotNull(oldCspWork.ConcurrencyStamp);

        var checkList = input.CheckList != null ? ObjectMapper.Map<List<CspWorkCheckListDto>, List<CspWorkCheckList>>(input.CheckList) : null;
        var notes = input.Notes != null ? ObjectMapper.Map<List<CspWorkNoteDto>, List<CspWorkNote>>(input.Notes) : null;

        var cspWork = await _manager.UpdateAsync(
            cspWork: oldCspWork,
            workCode: input.WorkCode.Trim(),
            name: input.Name.Trim(),
            taskCode: input.TaskCode.Trim(),
            startDate: input.StartDate,
            dueDate: input.DueDate,
            progress: input.Progress,
            duration: input.Duration,
            priority: input.Priority,
            complexity: input.Complexity,
            degreeOfImportant: input.DegreeOfImportant,
            description: input.Description,
            owner: input.Owner,
            assigner: input.Assigner,
            performer: input.Performer,
            collaborators: input.Collaborators,
            checkList: checkList,
            notes: notes,
            restrictComplete: input.RestrictComplete,
            projectId: input.ProjectId
            //parentId: input.ParentId
            );
        return ObjectMapper.Map<CspWork, CspWorkDto>(cspWork);
    }

    [Authorize]
    public override async Task DeleteAsync(Guid id)
    {
        var cspWork = await _repository.GetAsync(id);
        if (CurrentUser.Id != cspWork.Owner
            && !CurrentUser.Roles.Any(x => x == "admin"))
        {
            throw new BusinessException("AbpIdentity::DefaultErrorMessage403Detail");
        }
        await _manager.DeleteAsync(id);
    }

    [Authorize]
    public async Task<List<CspWorkDto>> GetChildrenAsync(Guid? parentId, bool recusive = false)
    {
        var cspWorks = await _repository.GetChildrenAsync(parentId, recusive);
        return ObjectMapper.Map<List<CspWork>, List<CspWorkDto>>(cspWorks);
    }

    [Authorize]
    public override async Task<PagedResultDto<CspWorkDto>> GetListAsync(CspWorkGetListInput input)
    {
        Guid? userId = input.UserId.HasValue ? input.UserId : CurrentUser.Id;
        if (CurrentUser.Roles.Any(x => x == "admin"))
        {
            userId = null;
        }
        var cspWork = await _repository.GetListAsync(
           input.Sorting,
           input.Keyword,
           input.WorkCode,
           input.TaskCode,
           input.Priority,
           input.ProjectId,
           input.ParentId,
           input.Status,
           input.OwnerId,
           input.AssignerId,
           input.PerformerId,
           input.CollaboratorId,
           userId,
           input.SkipCount,
           input.MaxResultCount,
           input.onlyRootNode);
        if (input.childIds != null && input.childIds.Count > 0)
        {
            foreach (var childId in input.childIds)
            {
                // tree not contain child
                if (!cspWork.Any(x => x.Id == childId))
                {
                    var child = await _repository.GetAsync(childId);
                    if (child != null)
                    {
                        cspWork.Add(child);
                        if (child.ParentId != null)
                        {
                            // tree not contain parent
                            if (!cspWork.Any(x => x.Id == child.ParentId))
                            {
                                var parents = await _repository.GetParentAsync(
                                    child.Code,
                                    child.Level,
                                    child.ParentId.Value,
                                    true,
                                    true);
                                cspWork.AddRange(parents);
                            }
                        }
                    }
                }
                cspWork = cspWork.DistinctBy<CspWork, Guid>(x => x.Id).ToList();
            }
        }
        var count = await _repository.GetCountAsync(
                                       input.Keyword,
                                       input.WorkCode,
                                       input.TaskCode,
                                       input.Priority,
                                       input.ProjectId,
                                       input.ParentId,
                                       input.Status,
                                       input.OwnerId,
                                       input.AssignerId,
                                       input.PerformerId,
                                       input.CollaboratorId,
                                       userId,
                                       input.onlyRootNode);
        return new PagedResultDto<CspWorkDto>()
        {
            TotalCount = count,
            Items = ObjectMapper.Map<List<CspWork>, List<CspWorkDto>>(cspWork)
        };
    }

    [HttpPut]
    [Authorize]
    public async Task<CspWorkDto> StartProgressAsync(Guid id)
    {
        var cspWork = await _repository.GetAsync(id);
        if (CurrentUser.Id != cspWork.Performer)
        {
            throw new BusinessException("AbpIdentity::DefaultErrorMessage403Detail");
        }
        cspWork.Status = CspWorkStatus.Progressing;
        cspWork.StartDate = DateTime.Now;
        await _repository.UpdateAsync(cspWork);
        return ObjectMapper.Map<CspWork, CspWorkDto>(cspWork);
    }

    [HttpPut]
    [Authorize]
    public async Task<CspWorkDto> HandOverAsync(Guid id, Guid perFormerId, string note)
    {
        var cspWork = await _repository.GetAsync(id);
        if (CurrentUser.Id != cspWork.Performer
            && CurrentUser.Id != cspWork.Owner
            && !CurrentUser.Roles.Any(x => x == "admin"))
        {
            throw new BusinessException("AbpIdentity::DefaultErrorMessage403Detail");
        }
        cspWork.Performer = perFormerId;
        var workNote = new CspWorkNote()
        {
            UserId = CurrentUser.Id,
            UserName = CurrentUser.UserName,
            Name = CurrentUser.Name,
            Surname = CurrentUser.SurName,
            NoteDate = DateTime.Now,
            Action = CspWorkAction.HandOver,
            Content = note
        };
        if (cspWork.Notes == null)
        {
            cspWork.Notes = new();
        }
        cspWork.Notes.Add(workNote);
        await _repository.UpdateAsync(cspWork);
        return ObjectMapper.Map<CspWork, CspWorkDto>(cspWork);
    }

    //Ban giao danh sach cong viec
    [HttpPut]
    [Authorize]
    public async Task<List<CspWorkDto>> UpdateLisWorkOfUserAsync(List<Guid> ids, Guid perFormerId, string note)
    {
        var cspWork = await _repository.GetListAllWork(ids);
        for (int i = 0; i <= cspWork.Count(); i++)
        {
            if (CurrentUser.Id != cspWork[i].Performer
                && CurrentUser.Id != cspWork[i].Owner
                && !CurrentUser.Roles.Any(x => x == "admin"))
            {
                throw new BusinessException("AbpIdentity::DefaultErrorMessage403Detail");
            }

            cspWork[i].Performer = perFormerId;
            var workNote = new CspWorkNote()
            {
                UserId = CurrentUser.Id,
                UserName = CurrentUser.UserName,
                Name = CurrentUser.Name,
                Surname = CurrentUser.SurName,
                NoteDate = DateTime.Now,
                Action = CspWorkAction.HandOver,
                Content = note
            };
            if (cspWork[i].Notes == null)
            {
                cspWork[i].Notes = new();
            }
            cspWork[i].Notes.Add(workNote);
        }
        await _repository.UpdateManyAsync(cspWork);
        return ObjectMapper.Map<List<CspWork>, List<CspWorkDto>>(cspWork);
    }

    //lay tat ca thanh vien trong du an
    public async Task<List<DropdownItemWork>> GetDropdownAsync(GetAllProjectMemberDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(CspWork.Members);
        }
        var memBer = await _repository.GetListAllAsync(input.Sorting,
            input.Filter
        );

        return ObjectMapper.Map<List<CspWork>, List<DropdownItemWork>>(memBer);
    }
}
