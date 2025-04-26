using CSP_NET.Work.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.WorkManagement;

public class CspWorkNoteDto
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public CspWorkAction Action { get; set; }
    public string Content { get; set; }
}
