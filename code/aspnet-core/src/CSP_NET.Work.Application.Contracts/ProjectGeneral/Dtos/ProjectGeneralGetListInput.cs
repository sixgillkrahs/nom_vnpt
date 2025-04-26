using System;
using System.ComponentModel;
using CTIN.Abp.Application.Dtos;

namespace CSP_NET.Work.ProjectGenerals.Dtos;

[Serializable]
public class ProjectGeneralGetListInput : PagedAndSortedResultRequestDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Department { get; set; }

    public string? Description { get; set; }

    public Guid? DepartmentID { get; set; }

    public Guid? ProjectStateID { get; set; }

    public bool? Status { get; set; }

    public string? Filter { get; set; }
}
public class GetAllProjectGeneralDto : ISortedResultRequest
{
    public string? Filter { get; set; }
    public string? Sorting { get; set; }
}
