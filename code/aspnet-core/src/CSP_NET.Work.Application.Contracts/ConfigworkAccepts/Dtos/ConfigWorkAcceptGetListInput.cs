using System;
using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;

namespace CSP_NET.Work.ConfigworkAccepts.Dtos;

//[Serializable]
public class ConfigWorkAcceptGetListInput : PagedAndSortedResultRequestDto
{
    public Guid WorkID { get; set; }
    public Guid UserAccept1 { get; set; }
    public DateTime? DateAccept1 { get; set; }
    public Guid? UserAccept2 { get; set; }
    public DateTime? DateAccept2 { get; set; }
    public WorkAcceptStatus StatusAccept1 { get; set; }
    public WorkAcceptStatus StatusAccept2 { get; set; }
    public bool Accept2 { get; set; }

    public string? Filter { get; set; }

}
public class GetAllConfigWorkAcceptDto : ISortedResultRequest
{
    public string? Filter { get; set; }
    public string? Sorting { get; set; }
}
public class ConfigWorkAcceptFilterInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

}