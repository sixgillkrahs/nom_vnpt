using CSP.Category.CategoryManagement;
using CSP_NET.Work.ProjectRole.Dtos;
using CTIN.Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ProjectRole.IServices
{
    public interface IProjectRoleAppService : ICrudAppService<ProjectRoleDto, Guid, GetListProjectRoleDto, CreateUpdateProjectRoleDto>
    {
        Task<List<DropdownItem>> GetDropdownAsync(GetAllProjectRoleDto input);
    }
}
