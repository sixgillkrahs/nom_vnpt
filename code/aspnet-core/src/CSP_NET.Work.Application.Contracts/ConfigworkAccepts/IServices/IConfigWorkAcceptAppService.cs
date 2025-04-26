using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSP_NET.Work.ConfigworkAccepts.Dtos;
using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Application.Services;
using CSP.Category.CategoryManagement;

namespace CSP_NET.Work.ConfigworkAccepts.IServices;

public interface IConfigWorkAcceptAppService :
    ICrudAppService<ConfigWorkAcceptDto, Guid, ConfigWorkAcceptGetListInput, CreateUpdateConfigWorkAcceptDto>
{
    Task<ConfigWorkAcceptDto> FindByWorkIdAsync(Guid WorkId);
}
