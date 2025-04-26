using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.WorkManagement;

public class CreateCspWorkDto : CreateUpdateCspWorkDto
{
    public Guid? ParentId { get; set; }
}
