using CSP_NET.Work.Common;
using System;
using System.Collections.Generic;

namespace CSP_NET.Work.ConfigworkAccepts.Dtos;

[Serializable]
public class CreateUpdateConfigWorkAcceptDto
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
