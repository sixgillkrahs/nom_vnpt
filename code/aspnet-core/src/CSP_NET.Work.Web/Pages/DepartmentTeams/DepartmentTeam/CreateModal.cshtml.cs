using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.DepartmentTeams;
using CSP_NET.Work.DepartmentTeams.Dtos;
using CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam.ViewModels;

namespace CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam;

public class CreateModalModel : WorkPageModel
{
    [BindProperty]
    public CreateEditDepartmentTeamViewModel ViewModel { get; set; }

    private readonly IDepartmentTeamAppService _service;

    public CreateModalModel(IDepartmentTeamAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditDepartmentTeamViewModel, CreateUpdateDepartmentTeamDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}
