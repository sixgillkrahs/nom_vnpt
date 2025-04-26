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
using CSP_NET.Work.Menu.Dtos;
using CSP_NET.Work.Menu.IServices;

namespace CSP_NET.Work.Menu;
public class MenuAppService : CrudAppService<Menu, MenuDto, Guid, GetListMenuDto, CreateUpdateMenuDto>, IMenuAppService
{
    private readonly MenuOptions _menuOptions;
    private readonly IDataFilter _dataFilter;
    private readonly IRepository<Menu, Guid> _repository;
    private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;

    public MenuAppService(IRepository<Menu, Guid> repository, IOptions<MenuOptions> menuOptions, IDataFilter dataFilter, IOpenIddictApplicationRepository openIddictApplicationRepository) : base(repository)
    {
        _menuOptions = menuOptions.Value;
        _dataFilter = dataFilter;
        _repository = repository;
        _openIddictApplicationRepository = openIddictApplicationRepository;
    }

    public override async Task<PagedResultDto<MenuDto>> GetListAsync(GetListMenuDto input)
    {
        if (!_menuOptions.MultiTenant)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var menus = await _repository.GetListAsync(x => x.ClientId == input.ClientId);
                return new PagedResultDto<MenuDto>
                {
                    Items = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus),
                    TotalCount = menus.Count
                };
            }
        }
        else
        {
            var menus = await _repository.GetListAsync(x => x.ClientId == input.ClientId);
            return new PagedResultDto<MenuDto>
            {
                Items = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus),
                TotalCount = menus.Count
            };
        }
    }

    public override async Task<MenuDto> CreateAsync(CreateUpdateMenuDto input)
    {
        if (input.ParentId != null)
        {
            var parentItem = await _repository.GetAsync(input.ParentId.Value);
            if (!parentItem.IsGroup)
                throw new BusinessException();
        }

        var menuItem = await _repository.InsertAsync(new Menu()
        {
            IconClass = input.IconClass,
            IsGroup = input.IsGroup,
            Label = input.Label,
            ParentId = input.ParentId,
            Order = input.Order,
            Layout = input.Layout,
            RequiredPolicy = input.RequiredPolicy,
            RouterLink = input.RouterLink,
            Url = input.Url,
            ClientId = input.ClientId
        });

        return ObjectMapper.Map<Menu, MenuDto>(menuItem);
    }

    public override async Task DeleteAsync(Guid id)
    {
        var allMenus = await _repository.GetListAsync();

        // Tìm menu cần xóa
        var menuToDelete = allMenus.FirstOrDefault(menu => menu.Id == id);

        if (menuToDelete != null)
        {
            // Tìm tất cả các con cháu của menu cần xóa
            var childMenusToDelete = FindChildMenus(allMenus, id);

            var allIdToDelete = childMenusToDelete.Select(x => x.Id).ToList();
            allIdToDelete.Add(id);

            // Xóa menu chính
            await _repository.DeleteManyAsync(allIdToDelete);
        }
    }

    // Phương thức đệ quy để tìm tất cả các con cháu của một menu
    private List<Menu> FindChildMenus(List<Menu> allMenus, Guid parentId)
    {
        var childMenus = new List<Menu>();

        foreach (var menu in allMenus)
        {
            if (menu.ParentId == parentId)
            {
                childMenus.Add(menu);
                // Tìm các con cháu của menu hiện tại
                var grandChildMenus = FindChildMenus(allMenus, menu.Id);
                childMenus.AddRange(grandChildMenus);
            }
        }

        return childMenus;
    }

    public async Task<List<string>> GetListClientId()
    {
        var clients = await _openIddictApplicationRepository.GetListAsync();
        return clients.Select(x => x.ClientId).ToList();
    }
}
