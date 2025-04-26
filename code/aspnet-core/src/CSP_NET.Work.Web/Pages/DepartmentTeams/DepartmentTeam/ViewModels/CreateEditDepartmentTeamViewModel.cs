using System;
using System.ComponentModel.DataAnnotations;

namespace CSP_NET.Work.Web.Pages.DepartmentTeams.DepartmentTeam.ViewModels;

public class CreateEditDepartmentTeamViewModel
{
    [Display(Name = "DepartmentTeamCode")]
    public string Code { get; set; }

    [Display(Name = "DepartmentTeamName")]
    public string Name { get; set; }

    [Display(Name = "DepartmentTeamDescription")]
    public string Description { get; set; }

    [Display(Name = "DepartmentTeamDepartmentID")]
    public Guid DepartmentID { get; set; }

    [Display(Name = "DepartmentTeamDepartment")]
    public OrganizationUnit Department { get; set; }

    [Display(Name = "DepartmentTeamStatus")]
    public RecordStatusCode Status { get; set; }

    [Display(Name = "DepartmentTeamUsers")]
    public List<IdentityUser> Users { get; set; }
}
