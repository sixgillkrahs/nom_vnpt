using System;
using System.ComponentModel.DataAnnotations;

namespace CSP_NET.Work.Web.Pages.ProjectState.ProjectState.ViewModels;

public class CreateEditProjectStateViewModel
{
    [Display(Name = "ProjectStateCode")]
    public string Code { get; set; }

    [Display(Name = "ProjectStateName")]
    public string Name { get; set; }
}
