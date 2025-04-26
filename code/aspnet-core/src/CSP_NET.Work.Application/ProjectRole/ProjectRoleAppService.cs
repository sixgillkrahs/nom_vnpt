using CSP.Category.CategoryManagement;
using CSP_NET.Work.Menu;
using CSP_NET.Work.ProjectRole.Dtos;
using CSP_NET.Work.ProjectRole.IServices;
using CTIN.Abp;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CTIN.Abp.Data;
using CTIN.Abp.Domain.Repositories;
using CTIN.Abp.ObjectMapping;
using CTIN.Abp.OpenIddict.Applications;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectRole
{
    public class ProjectRoleAppService : CrudAppService<ProjectRole, ProjectRoleDto, Guid, GetListProjectRoleDto, CreateUpdateProjectRoleDto>, IProjectRoleAppService
    {
        private readonly ProjectRoleManager _projectRoleManager;
        private readonly IDataFilter _dataFilter;
        private readonly IProjectRoleRepository _repository;
        private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

        public ProjectRoleAppService(IProjectRoleRepository repository,ProjectRoleManager projectRoleManager, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
        {
            
            _dataFilter = dataFilter;
            _projectRoleManager = projectRoleManager;
            _repository = repository;
            _openIddictApplicationRepository = openIddictApplicationRepository;
        }

        public override async Task<PagedResultDto<ProjectRoleDto>> GetListAsync(GetListProjectRoleDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ProjectRole.Name);
            }
            var projectRoles = await _repository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _repository.CountAsync()
                : await _repository.CountAsync(table => table.Name.Contains(input.Filter) || table.Code.Contains(input.Filter));
            return new PagedResultDto<ProjectRoleDto>(
                totalCount,
                ObjectMapper.Map<List<ProjectRole>, List<ProjectRoleDto>>(projectRoles)
            );
        }

        public override async Task<ProjectRoleDto> GetAsync(Guid id)
        {
            var projectRole = await _repository.FindByIdAsync(id);
            if (projectRole == null)
            {

                throw new UserFriendlyException("ProjectRole không tồn tại");

            }
            return ObjectMapper.Map<ProjectRole, ProjectRoleDto>(projectRole);
        }

        public override async Task<ProjectRoleDto> CreateAsync(CreateUpdateProjectRoleDto input)
        {
            var projectRoles = await _projectRoleManager.CreateAsync(
                input.Code,
                input.Name,
                input.Domain,
                input.Status

            );
            await _repository.InsertAsync(projectRoles);
            return ObjectMapper.Map<ProjectRole, ProjectRoleDto>(projectRoles);
        }
        public async Task <ProjectRoleDto> UpdateRootAsync(Guid id, CreateUpdateProjectRoleDto input)
        {
            var roles = await _repository.FindByIdAsync(id);
            if (roles != null)
            {

                if (roles.Code != input.Code)
                {
                    await _projectRoleManager.ChangeCodeAsync(roles, input.Code);
                }

                if (roles.Name != input.Name)
                {
                    await _projectRoleManager.ChangeNameAsync(roles, input.Name);
                }

                await _repository.UpdateAsync(roles);
            }
            return ObjectMapper.Map<ProjectRole, ProjectRoleDto>(roles);
        }
        public async Task<List<DropdownItem>> GetDropdownAsync(GetAllProjectRoleDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ProjectRole.Code);
            }
            var activeStatuss = await _repository.GetListAllAsync(input.Sorting,
                input.Filter
            );

            return ObjectMapper.Map<List<ProjectRole>, List<DropdownItem>>(activeStatuss);
        }
    }
}
