using System;
using CTIN.Abp.Application.Dtos;
using CSP_NET.Work.Common;
namespace CSP_NET.Work.ConfigworkAccepts.Dtos;


public class ConfigWorkAcceptDto : EntityDto<Guid>
{
    public Guid WorkID { get; set; }
    public Guid UserAccept1 { get; set; }
    public DateTime? DateAccept1 { get; set; }
    public Guid? UserAccept2 { get; set; }
    public DateTime? DateAccept2 { get; set; }
    public WorkAcceptStatus StatusAccept1 { get; set; }
    public WorkAcceptStatus StatusAccept2 { get; set; }
    public bool Accept2 { get; set; }
  
}
public class ConfigWorkAcceptListDto : EntityDto<Guid>
{
    public Guid WorkID { get; set; }
    public Guid UserAccept1 { get; set; }
    public DateTime? DateAccept1 { get; set; }
    public Guid? UserAccept2 { get; set; }
    public DateTime? DateAccept2 { get; set; }
    public WorkAcceptStatus StatusAccept1 { get; set; }
    public WorkAcceptStatus StatusAccept2 { get; set; }
    public bool Accept2 { get; set; }
}
