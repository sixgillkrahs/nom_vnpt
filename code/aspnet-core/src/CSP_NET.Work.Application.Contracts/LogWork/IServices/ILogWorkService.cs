using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.ConfigworkAccepts.Dtos;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CSP.Category.CategoryManagement;
using CSP_NET.Work.LogWorks.Dtos;

namespace CSP_NET.Work.LogWorks.IServices;

public interface ILogWorkAppService :
ICrudAppService<LogWorkDto, Guid, LogWorkGetListInput, CreateUpdateLogWorkDto>
{
    Task<LogWorkDto> FindByWorkIdAsync(Guid WorkId);
}
