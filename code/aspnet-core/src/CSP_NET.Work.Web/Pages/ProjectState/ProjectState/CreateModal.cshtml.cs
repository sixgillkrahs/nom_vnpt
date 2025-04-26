using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.ProjectState;
using CSP_NET.Work.ProjectState.Dtos;
using CSP_NET.Work.Web.Pages.ProjectState.ProjectState.ViewModels;

namespace CSP_NET.Work.Web.Pages.ProjectState.ProjectState;

public class CreateModalModel : WorkPageModel
{
    [BindProperty]
    public CreateEditProjectStateViewModel ViewModel { get; set; }

    private readonly IProjectStateAppService _service;

    public CreateModalModel(IProjectStateAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditProjectStateViewModel, CreateUpdateProjectStateDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}
