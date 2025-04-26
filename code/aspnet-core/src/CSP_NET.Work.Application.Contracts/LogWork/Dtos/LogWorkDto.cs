using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using CSP_NET.Work.WorkManagement;
namespace CSP_NET.Work.LogWorks.Dtos;

public class  LogWorkDto : EntityDto<Guid>
{
    public Guid WorkId { get; set; }
    public Guid UserId { get; set; }
    public string Time { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public StatusLogWork StatusLogWork { get; set; }

}
public class LogWorkListDto : EntityDto<Guid>
{
    public Guid WorkId { get; set; }
    public Guid UserId { get; set; }
    public string Time { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public StatusLogWork StatusLogWork { get; set; }
}
