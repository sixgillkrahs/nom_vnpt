using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.Common;
using CSP_NET.Work.WorkManagement;
using CTIN.Abp.Application.Services;

namespace CSP_NET.Work.WorkManagement;


public interface ICspWorkAppService :
    ICrudAppService< 
        CspWorkDto, 
        Guid, 
        CspWorkGetListInput,
        CreateCspWorkDto,
        UpdateCspWorkDto>
{
    Task<List<CspWorkDto>> GetChildrenAsync(Guid? parentId, bool recusive = false);
    Task<CspWorkDto> HandOverAsync(Guid id, Guid perFormerId, string note);
    Task<CspWorkDto> StartProgressAsync(Guid id);
}
