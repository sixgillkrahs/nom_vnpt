using System;
using System.ComponentModel.DataAnnotations;

namespace CSP_NET.Work.Web.Pages.ProjectGeneral.ProjectGeneral.ViewModels;

public class CreateEditProjectGeneralViewModel
{
    [Display(Name = "ProjectGeneralCode")]
    public string Code { get; set; }

    [Display(Name = "ProjectGeneralName")]
    public string Name { get; set; }

    [Display(Name = "ProjectGeneralDepartment")]
    public string Department { get; set; }

    [Display(Name = "ProjectGeneralState")]
    public string State { get; set; }

    [Display(Name = "ProjectGeneralStatus")]
    public bool Status { get; set; }
}
