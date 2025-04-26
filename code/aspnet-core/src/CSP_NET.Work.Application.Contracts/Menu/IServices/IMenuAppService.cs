using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTIN.Abp;
using CTIN.Abp.Application.Services;
using CSP_NET.Work.Menu.Dtos;

namespace CSP_NET.Work.Menu.IServices;
public interface IMenuAppService : ICrudAppService<MenuDto, Guid, GetListMenuDto, CreateUpdateMenuDto>
{
    Task<List<string>> GetListClientId();
}
