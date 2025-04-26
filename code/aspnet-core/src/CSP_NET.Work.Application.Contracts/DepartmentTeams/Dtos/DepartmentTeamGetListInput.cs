using System;
using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;

namespace CSP_NET.Work.DepartmentTeams.Dtos;

//[Serializable]
public class DepartmentTeamGetListInput : PagedAndSortedResultRequestDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public Guid? DepartmentID { get; set; }

    public RecordStatusCode Status { get; set; }

    public string? Filter { get; set; }

}
public class GetAllDepartmentTeamDto : ISortedResultRequest
{
    public string? Filter { get; set; }
    public Guid? DepartmentID { get; set; }
    public string? Sorting { get; set; }
}
public class DepartmentTeamFilterInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public Guid? DepartmentID { get; set; }

}