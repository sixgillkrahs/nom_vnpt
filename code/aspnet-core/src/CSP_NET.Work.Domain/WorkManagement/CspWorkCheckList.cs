using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.WorkManagement;

[Owned]
public class CspWorkCheckList
{
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
