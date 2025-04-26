using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.DepartmentTeams.Dtos;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CSP.Category.CategoryManagement;
namespace CSP_NET.Work.DepartmentTeams.IServices;


public interface IDepartmentTeamAppService :
    ICrudAppService<
        DepartmentTeamDto,
        //DepartmentTeamListDto,
        Guid,
        DepartmentTeamGetListInput,
        CreateUpdateDepartmentTeamDto>
{
    
}
