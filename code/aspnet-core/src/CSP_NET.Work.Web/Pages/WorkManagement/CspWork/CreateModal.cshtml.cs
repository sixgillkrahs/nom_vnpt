using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.WorkManagement;
using CSP_NET.Work.WorkManagement.Dtos;
using CSP_NET.Work.Web.Pages.WorkManagement.CspWork.ViewModels;

namespace CSP_NET.Work.Web.Pages.WorkManagement.CspWork;

public class CreateModalModel : WorkPageModel
{
    [BindProperty]
    public CreateEditCspWorkViewModel ViewModel { get; set; }

    private readonly ICspWorkAppService _service;

    public CreateModalModel(ICspWorkAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditCspWorkViewModel, CreateUpdateCspWorkDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}
