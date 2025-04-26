using System;
using CSP.Category.CategoryManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.ProjectGenerals.Dtos;
using CTIN.Abp.Application.Services;

namespace CSP_NET.Work.ProjectGenerals.IServices;


public interface IProjectGeneralAppService :
    ICrudAppService<
        ProjectGeneralDto,
        Guid,
        ProjectGeneralGetListInput,
        CreateUpdateProjectGeneralDto,
        CreateUpdateProjectGeneralDto>
{
    Task<List<DropdownItem>> GetDropdownAsync(GetAllProjectGeneralDto input);

    Task<ProjectGeneralDto> SyncProjectAsync(string code);
}
