using System;

namespace CSP_NET.Work.ProjectGenerals.Dtos;

[Serializable]
public class CreateUpdateProjectGeneralDto
{
    public string Code { get; set; }

    public string Name { get; set; }

    public Guid DepartmentID { get; set; }

    public string? Description { get; set; }

    public Guid? ProjectStateID { get; set; }

    public bool Status { get; set; }
}
