using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSP_NET.Work.ProjectState;
using CSP_NET.Work.ProjectState.Dtos;
using CSP_NET.Work.Web.Pages.ProjectState.ProjectState.ViewModels;

namespace CSP_NET.Work.Web.Pages.ProjectState.ProjectState;

public class EditModalModel : WorkPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditProjectStateViewModel ViewModel { get; set; }

    private readonly IProjectStateAppService _service;

    public EditModalModel(IProjectStateAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<ProjectStateDto, CreateEditProjectStateViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditProjectStateViewModel, CreateUpdateProjectStateDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}
