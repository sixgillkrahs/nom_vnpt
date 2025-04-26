using CSP_NET.Work.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.WorkManagement;
[Owned]
public class CspWorkNote
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public CspWorkAction Action { get; set; }
    public string Content { get; set; }
    public DateTime NoteDate { get; set; }
}
