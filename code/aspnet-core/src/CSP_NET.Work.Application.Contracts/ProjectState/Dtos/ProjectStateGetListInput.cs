using System;
using System.ComponentModel;
using CTIN.Abp.Application.Dtos;

namespace CSP_NET.Work.ProjectState.Dtos;

[Serializable]
public class ProjectStateGetListInput : PagedAndSortedResultRequestDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Filter { get; set; }
}
public class GetAllProjectStateDto : ISortedResultRequest
{
    public string? Filter { get; set; }
    public string? Sorting { get; set; }
}