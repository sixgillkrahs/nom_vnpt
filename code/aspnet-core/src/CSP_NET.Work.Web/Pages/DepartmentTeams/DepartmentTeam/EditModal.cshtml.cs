using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.DepartmentTeams;
using CSP_NET.Work.DepartmentTeams.Dtos;
using CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam.ViewModels;

namespace CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam;

public class EditModalModel : WorkPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditDepartmentTeamViewModel ViewModel { get; set; }

    private readonly IDepartmentTeamAppService _service;

    public EditModalModel(IDepartmentTeamAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<DepartmentTeamDto, CreateEditDepartmentTeamViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditDepartmentTeamViewModel, CreateUpdateDepartmentTeamDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}
