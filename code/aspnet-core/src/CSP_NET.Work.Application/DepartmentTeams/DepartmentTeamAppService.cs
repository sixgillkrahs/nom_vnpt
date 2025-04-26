using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTIN.Abp;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.MultiTenancy;
using CTIN.Abp.OpenIddict.Applications;
using Microsoft.Extensions.Options;
using CSP_NET.Work.DepartmentTeams.Dtos;
using CSP_NET.Work.DepartmentTeams.IServices;
using CSP_NET.Work.DepartmentTeamMembers;
using CTIN.Abp.ObjectMapping;
using Microsoft.AspNetCore.Components.Forms;
namespace CSP_NET.Work.DepartmentTeams;


public class DepartmentTeamAppService : CrudAppService<DepartmentTeam, DepartmentTeamDto, Guid, DepartmentTeamGetListInput, CreateUpdateDepartmentTeamDto>, IDepartmentTeamAppService
{

    private readonly IDataFilter _dataFilter;
    private readonly IDepartmentTeamRepository _repository;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;
    //private readonly IDepartmentTeamRepository<DepartmentTeam, Guid> _repositoryDepartment;


    public DepartmentTeamAppService(IDepartmentTeamRepository repository, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
    {
        _dataFilter = dataFilter;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
    }

    //protected async Task<IQueryable<DepartmentTeam>> CreateFilteredQueryAsync(DepartmentTeamGetListInput input)
    //{
    //    return (await base.CreateFilteredQueryAsync(input))
    //        .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
    //        .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
    //        .WhereIf(input.DepartmentID.HasValue, x => x.DepartmentID == input.DepartmentID.Value) // Sử dụng Equals
    //        .WhereIf(input.Status != null, x => x.Status == input.Status);
    //}

    public async Task<PagedResultDto<DepartmentTeamDto>> GetListClientId(DepartmentTeamFilterInput input)
    {

        var teams = await _repository.GetListAllAsync(input.Sorting, input.Filter);

        var teamSearch = teams
        .Where(x =>
                (input.Filter.IsNullOrWhiteSpace() || x.Code.Contains(input.Filter) || x.Name.Contains(input.Filter)) &&
                (!input.DepartmentID.HasValue || x.DepartmentID == input.DepartmentID.Value)
            )
            .ToList();
        var teamDtos = ObjectMapper.Map<List<DepartmentTeam>, List<DepartmentTeamDto>>(teamSearch);
        return new PagedResultDto<DepartmentTeamDto>
        {
            Items = (IReadOnlyList<DepartmentTeamDto>)ObjectMapper.Map<List<DepartmentTeam>, List<DepartmentTeamDto>>(teamSearch),
            TotalCount = teamSearch.Count
        };
    }


    Task<DepartmentTeamDto> IReadOnlyAppService<DepartmentTeamDto, DepartmentTeamDto, Guid, DepartmentTeamGetListInput>.GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<PagedResultDto<DepartmentTeamDto>> IReadOnlyAppService<DepartmentTeamDto, DepartmentTeamDto, Guid, DepartmentTeamGetListInput>.GetListAsync(DepartmentTeamGetListInput input)
    {
        throw new NotImplementedException();
    }

    public override async Task<DepartmentTeamDto> GetAsync(Guid id)
    {
        var team = await _repository.FindByIdAsync(id);
        return ObjectMapper.Map<DepartmentTeam, DepartmentTeamDto>(team);
    }
    public override async Task<DepartmentTeamDto> CreateAsync(CreateUpdateDepartmentTeamDto input)
    {
        var departmentTeam = await _repository.InsertAsync(new DepartmentTeam()
        {
            Code = input.Code,
            Name = input.Name,
            Description = input.Description,
            DepartmentID = input.DepartmentID,
            Status = Common.RecordStatusCode.active,
            Members = input.Users.Select(id =>
            {
                return new DepartmentTeamMember()
                {
                    UserId = id
                };
            }).ToList()

        });
        //return new DepartmentTeamDto();
        return ObjectMapper.Map<DepartmentTeam, DepartmentTeamDto>(departmentTeam);
    }

    public async override Task<DepartmentTeamDto> UpdateAsync(Guid id, CreateUpdateDepartmentTeamDto input)
    {
        var team = await _repository.FindByIdAsync(id);
        ObjectMapper.Map(input, team);
        await _repository.UpdateAsync(team);

        // Xóa và thêm lại thành viên (quản lý ở phương thức riêng)
        await ManageTeamMembersAsync(id, input.Users);

        return ObjectMapper.Map<DepartmentTeam, DepartmentTeamDto>(team);
    }
    public async override Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        // Có thể xóa các TeamMembers liên quan ở đây nếu cần
    }


    private async Task ManageTeamMembersAsync(Guid teamId, List<Guid> memberUserIds)
    {
        var team = await _repository.FindByIdAsync(teamId);
        //team.Members = new List<DepartmentTeamMember>();
        if (team != null)
        {
            if (team.Members != null)
            {
                team.Members.Clear();
            }
        }

        // Thêm mới các thành viên mới của Team
        var teamMembers = memberUserIds.Select(userId => new DepartmentTeamMember()
        {
            UserId = userId,
        }).ToList();
        team.Members = (ICollection<DepartmentTeamMember>?)teamMembers;
        await _repository.UpdateAsync(team);
    }

}
