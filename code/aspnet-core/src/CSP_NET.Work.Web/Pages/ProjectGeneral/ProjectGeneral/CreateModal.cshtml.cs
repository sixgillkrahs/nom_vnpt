using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.ProjectGeneral;
using CSP_NET.Work.ProjectGeneral.Dtos;
using CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral.ViewModels;

namespace CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral;

public class CreateModalModel : WorkPageModel
{
    [BindProperty]
    public CreateEditProjectGeneralViewModel ViewModel { get; set; }

    private readonly IProjectGeneralAppService _service;

    public CreateModalModel(IProjectGeneralAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditProjectGeneralViewModel, CreateUpdateProjectGeneralDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}
