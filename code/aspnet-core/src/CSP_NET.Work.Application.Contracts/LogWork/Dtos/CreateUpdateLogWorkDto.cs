using CSP_NET.Work.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.LogWorks.Dtos;
[Serializable]

public class CreateUpdateLogWorkDto
{
    public Guid WorkId { get; set; }
    public Guid UserId { get; set; }
    public string Time { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public StatusLogWork StatusLogWork { get; set; }
}

