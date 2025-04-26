using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSP.Category.CategoryManagement;
using CSP_NET.Work.Permissions;
using CSP_NET.Work.ProjectGenerals;
using CSP_NET.Work.ProjectState.Dtos;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.ObjectMapping;
using CTIN.Abp.Data;
using CTIN.Abp.OpenIddict.Applications;
using CSP_NET.Work.Common;

namespace CSP_NET.Work.ProjectState;

public class ProjectStateAppService : CrudAppService<ProjectState, ProjectStateDto, Guid, ProjectStateGetListInput, CreateUpdateProjectStateDto, CreateUpdateProjectStateDto>,
    IProjectStateAppService
{
    protected override string GetPolicyName { get; set; } = WorkPermissions.ProjectState.Default;
    protected override string GetListPolicyName { get; set; } = WorkPermissions.ProjectState.Default;
    protected override string CreatePolicyName { get; set; } = WorkPermissions.ProjectState.Create;
    protected override string UpdatePolicyName { get; set; } = WorkPermissions.ProjectState.Update;
    protected override string DeletePolicyName { get; set; } = WorkPermissions.ProjectState.Delete;

    private readonly ProjectStateManager _projectStateManager;
    private readonly IDataFilter _dataFilter;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

    private readonly IProjectStateRepository _repository;

    public ProjectStateAppService(IProjectStateRepository repository, ProjectStateManager projectGeneralManager, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
    {
        _dataFilter = dataFilter;
        _projectStateManager = projectGeneralManager;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
        _repository = repository;
    }

    protected override async Task<IQueryable<ProjectState>> CreateFilteredQueryAsync(ProjectStateGetListInput input)
    {
        // TODO: CTIN.Csp.CliHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            ;
    }

    public override async Task<PagedResultDto<ProjectStateDto>> GetListAsync(ProjectStateGetListInput input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(ProjectState.Name);
        }
        var projectState = await _repository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );
        var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(table => table.Name.Contains(input.Filter) || table.Code.Contains(input.Filter));
        return new PagedResultDto<ProjectStateDto>(
            totalCount,
            ObjectMapper.Map<List<ProjectState>, List<ProjectStateDto>>(projectState)
        );
    }

    public override async Task<ProjectStateDto> GetAsync(Guid id)
    {
        var state = await _repository.FindByIdAsync(id);
        if (state == null)
        {

            throw new UserFriendlyException("Giai doan khong ton tai");

        }
        return ObjectMapper.Map<ProjectState, ProjectStateDto>(state);
    }

    public override async Task<ProjectStateDto> CreateAsync(CreateUpdateProjectStateDto input)
    {
        var project = await _projectStateManager.CreateAsync(
            input.Code,
            input.Name

        );
        await _repository.InsertAsync(project);
        return ObjectMapper.Map<ProjectState, ProjectStateDto>(project);
    }

    public async Task UpdateRootAsync(Guid id, CreateUpdateProjectStateDto input)
    {
        var project = await _repository.FindByIdAsync(id);
        if (project != null)
        {

            if (project.Code != input.Code)
            {
                await _projectStateManager.ChangeCodeAsync(project, input.Code);
            }

            if (project.Name != input.Name)
            {
                await _projectStateManager.ChangeNameAsync(project, input.Name);
            }

            await _repository.UpdateAsync(project);

            //  return ObjectMapper.Map<ProjectGeneral, ProjectGeneralDto>(project);
        }
    }
    

    public override async Task DeleteAsync(Guid id)
    {
        var allProject = await _repository.GetListAsync();


        var projectGarenal = allProject.FirstOrDefault(project => project.Id == id);

        if (projectGarenal != null)
        {


            await _repository.DeleteAsync(projectGarenal);
        }
    }

    public async Task<List<DropdownItemWork>> GetDropdownAsync(GetAllProjectStateDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(ProjectState.Code);
        }
        var activeStatuss = await _repository.GetListAllAsync(input.Sorting,
            input.Filter
        );

        return ObjectMapper.Map<List<ProjectState>, List<DropdownItemWork>>(activeStatuss);
    }
}


