using System;
using CTIN.Abp.Application.Dtos;
using CSP_NET.Work.ProjectState;
using static CTIN.Abp.Identity.Settings.IdentitySettingNames;

namespace CSP_NET.Work.ProjectGenerals.Dtos;

[Serializable]
public class ProjectGeneralDto : EntityDto<Guid>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public Guid DepartmentID { get; set; }

    public _OrganizationUnit Department { get; set; }

    public string? Description { get; set; }

    public Guid? ProjectStateID { get; set; }

    public  _ProjectStateDto ProjectState { get; set; }

    public bool Status { get; set; }
}

public class _ProjectStateDto
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class _OrganizationUnit
{
    public virtual string DisplayName { get; set; }
}
