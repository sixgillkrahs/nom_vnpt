using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSP.Category.CategoryManagement;
using CSP_NET.Work.Permissions;
using CSP_NET.Work.ProjectGenerals.Dtos;
using CSP_NET.Work.ProjectGenerals.IServices;
using CSP_NET.Work.ProjectRole;
using CSP_NET.Work.ProjectState;
using CSP_NET.Work.SyncCtin;
using CTIN.Abp;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.ObjectMapping;
using CTIN.Abp.OpenIddict.Applications;

namespace CSP_NET.Work.ProjectGenerals;

public class ProjectGeneralAppService : CrudAppService<ProjectGeneral, ProjectGeneralDto, Guid, ProjectGeneralGetListInput, CreateUpdateProjectGeneralDto, CreateUpdateProjectGeneralDto>,
IProjectGeneralAppService
{
    protected override string GetPolicyName { get; set; } = WorkPermissions.ProjectGeneral.Default;
    protected override string GetListPolicyName { get; set; } = WorkPermissions.ProjectGeneral.Default;
    protected override string CreatePolicyName { get; set; } = WorkPermissions.ProjectGeneral.Create;
    protected override string UpdatePolicyName { get; set; } = WorkPermissions.ProjectGeneral.Update;
    protected override string DeletePolicyName { get; set; } = WorkPermissions.ProjectGeneral.Delete;

    private readonly ProjectGeneralManager _projectGeneralManager;
    private readonly IDataFilter _dataFilter;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

    private readonly IProjectGeneralRepository _repository;
    private readonly IProjectStateRepository _projectStateRepository;

    private readonly IPMSCTIN_HTTPClient _pmsHTTPClient;

    public ProjectGeneralAppService(IProjectGeneralRepository repository, ProjectGeneralManager projectGeneralManager, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository, IPMSCTIN_HTTPClient pmsHTTPClient, IProjectStateRepository projectStateRepository) : base(repository)
    {
        _dataFilter = dataFilter;
        _projectGeneralManager = projectGeneralManager;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
        _pmsHTTPClient = pmsHTTPClient;
        _projectStateRepository = projectStateRepository;
    }

    protected override async Task<IQueryable<ProjectGeneral>> CreateFilteredQueryAsync(ProjectGeneralGetListInput input)
    {
        // TODO: CTIN.Csp.CliHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.DepartmentID != null, x => x.DepartmentID == input.DepartmentID)
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            .WhereIf(input.ProjectStateID != null, x => x.ProjectStateID == input.ProjectStateID)
            .WhereIf(input.Status != null, x => x.Status == input.Status)
            ;
    }
    public override async Task<PagedResultDto<ProjectGeneralDto>> GetListAsync(ProjectGeneralGetListInput input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(ProjectGeneral.Name);
        }
        var projectGeneral = await _repository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            true
        );
        var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(table => table.Name.Contains(input.Filter) || table.Code.Contains(input.Filter));
        return new PagedResultDto<ProjectGeneralDto>(
            totalCount,
            ObjectMapper.Map<List<ProjectGeneral>, List<ProjectGeneralDto>>(projectGeneral)
        );
    }

    public override async Task<ProjectGeneralDto> GetAsync(Guid id)
    {
        var project = await _repository.FindByIdAsync(id);
        if (project == null)
        {

            throw new UserFriendlyException("Dự án không tồn tại");

        }
        return ObjectMapper.Map<ProjectGeneral, ProjectGeneralDto>(project);
    }

    public override async Task<ProjectGeneralDto> CreateAsync(CreateUpdateProjectGeneralDto input)
    {
        var project = await _projectGeneralManager.CreateAsync(
            input.Code,
            input.Name,
            input.DepartmentID,
            input.Description,
            input.ProjectStateID,
            input.Status

        );
        await _repository.InsertAsync(project);

        //var project = await _repository.InsertAsync(new ProjectGeneral()
        //{
        //    Code = input.Code,
        //    Name = input.Name,
        //    Description = input.Description,
        //    DepartmentID = input.DepartmentID,
        //    State = input.State,
        //    Status = input.Status,
        //});
        return ObjectMapper.Map<ProjectGeneral, ProjectGeneralDto>(project);
    }

    public async Task UpdateRootAsync(Guid id, CreateUpdateProjectGeneralDto input)
    {
        var project = await _repository.FindByIdAsync(id);
        if (project != null)
        {

            if (project.Code != input.Code)
            {
                await _projectGeneralManager.ChangeCodeAsync(project, input.Code);
            }

            if (project.Name != input.Name)
            {
                await _projectGeneralManager.ChangeNameAsync(project, input.Name);
            }

            ObjectMapper.Map<CreateUpdateProjectGeneralDto, ProjectGeneral>(input, project);

            await _repository.UpdateAsync(project);
        }
    }
    public async Task<List<DropdownItem>> GetDropdownAsync(GetAllProjectGeneralDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(ProjectGeneral.Code);
        }
        var activeStatuss = await _repository.GetListAllAsync(input.Sorting,
            input.Filter
        );

        return ObjectMapper.Map<List<ProjectGeneral>, List<DropdownItem>>(activeStatuss);
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

    public async Task<ProjectGeneralDto> SyncProjectAsync(string code)
    {

        var oldProject = await _repository.FindByCodeAsync(code);
        if (oldProject == null)
        {
            var pmsProject = await _pmsHTTPClient.GetProject(code);

            if (pmsProject == null)
                throw new BusinessException(WorkDomainErrorCodes.NotFindPmsProjectWithCode).WithData("code", code);

            var pmsState = await _projectStateRepository.FindAsync(new Guid("f4303a14-02f3-b798-b691-3a10dbded1f7"));

            if (pmsState == null)
            {
                var pmsStates = new List<ProjectState.ProjectState>()
                {
                    new ProjectState.ProjectState(new Guid("f4303a14-02f3-b798-b691-3a10dbded1f7"))
                    {
                        Code = "Initialize",
                        Name = "Khởi tạo dự án"
                    },
                    new ProjectState.ProjectState(new Guid("7925dde2-8b87-3dbb-e6df-3a10dbdf08c0"))
                    {
                        Code = "PreBidding",
                        Name = "Tiền đấu thầu"
                    },
                    new ProjectState.ProjectState(new Guid("71117775-c1d3-2d72-d7f3-3a10dbdfe3fb"))
                    {
                        Code = "Bidding",
                        Name = "Đấu thầu"
                    },
                    new ProjectState.ProjectState(new Guid("20169e92-f887-7aea-27a1-3a10dbe0182d"))
                    {
                        Code = "Negotiate",
                        Name = "Đàm phán hợp đồng"
                    },
                    new ProjectState.ProjectState(new Guid("571fc5d2-38b4-4511-9cc8-3a10dbe0456e"))
                    {
                        Code = "Contract",
                        Name = "Kí kết hợp đồng"
                    },
                    new ProjectState.ProjectState(new Guid("631fa3aa-9e44-0b15-5194-3a10dbe1004a"))
                    {
                        Code = "Deployment",
                        Name = "Triển khai"
                    },
                    new ProjectState.ProjectState(new Guid("42712087-01b8-3a0e-ce5a-3a10dbe19c87"))
                    {
                        Code = "TechnicalSupport",
                        Name = "Hỗ trợ kỹ thuật"
                    },
                    new ProjectState.ProjectState(new Guid("9a03181c-5fab-d72c-8d91-3a10dbe1cb32"))
                    {
                        Code = "Đóng dự án",
                        Name = "Close"
                    }
                };

                await _projectStateRepository.InsertManyAsync(pmsStates);
            }

            // map tạm thời state từ pms sang work
            Guid[] pmsStateIds = new Guid[]
            {
                new Guid("f4303a14-02f3-b798-b691-3a10dbded1f7"),
                new Guid("7925dde2-8b87-3dbb-e6df-3a10dbdf08c0"),
                new Guid("71117775-c1d3-2d72-d7f3-3a10dbdfe3fb"),
                new Guid("20169e92-f887-7aea-27a1-3a10dbe0182d"),
                new Guid("571fc5d2-38b4-4511-9cc8-3a10dbe0456e"),
                new Guid("631fa3aa-9e44-0b15-5194-3a10dbe1004a"),
                new Guid("42712087-01b8-3a0e-ce5a-3a10dbe19c87"),
                new Guid("9a03181c-5fab-d72c-8d91-3a10dbe1cb32")
            };

            var newProject = await _repository.InsertAsync(new ProjectGeneral(pmsProject.id)
            {
                Code = pmsProject.projCode,
                Name = pmsProject.projName,
                DepartmentID = SyncCommon.Int2Guid(pmsProject.departmentId),
                ProjectStateID = pmsStateIds[pmsProject.projPhase]
            });
            return ObjectMapper.Map<ProjectGeneral, ProjectGeneralDto>(newProject);
        }
        else throw new BusinessException(WorkDomainErrorCodes.ProjectHasBeenSynchronizedBefore).WithData("code", code);
    }
}


