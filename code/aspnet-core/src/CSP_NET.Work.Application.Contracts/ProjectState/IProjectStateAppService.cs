using System;
using CSP.Category.CategoryManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.ProjectState.Dtos;
using CTIN.Abp.Application.Services;
using CSP_NET.Work.Common;

namespace CSP_NET.Work.ProjectState;


public interface IProjectStateAppService :
    ICrudAppService< 
        ProjectStateDto, 
        Guid, 
        ProjectStateGetListInput,
        CreateUpdateProjectStateDto,
        CreateUpdateProjectStateDto>
{
    Task<List<DropdownItemWork>> GetDropdownAsync(GetAllProjectStateDto input);
}
