using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.ProjectGeneral;
using CSP_NET.Work.ProjectGeneral.Dtos;
using CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral.ViewModels;

namespace CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral;

public class EditModalModel : WorkPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditProjectGeneralViewModel ViewModel { get; set; }

    private readonly IProjectGeneralAppService _service;

    public EditModalModel(IProjectGeneralAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<ProjectGeneralDto, CreateEditProjectGeneralViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditProjectGeneralViewModel, CreateUpdateProjectGeneralDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}
