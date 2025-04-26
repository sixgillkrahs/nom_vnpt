using CSP_NET.Work.Common;
using System;
using System.Collections.Generic;

namespace CSP_NET.Work.DepartmentTeams.Dtos;

[Serializable]
public class CreateUpdateDepartmentTeamDto
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid DepartmentID { get; set; }

    public List<Guid> Users { get; set; }
}
