using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.LogWorks.Dtos;

public class LogWorkGetListInput : PagedAndSortedResultRequestDto
{
    public Guid WorkId { get; set; }
    public Guid UserId { get; set; }
    public string Time { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public StatusLogWork StatusLogWork { get; set; }
    public string? Filter { get; set; }


}

public class GetAllLogWorkDto : ISortedResultRequest
{
    public string? Filter { get; set; }
    public string? Sorting { get; set; }
}
public class LogWorkFilterInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

}
