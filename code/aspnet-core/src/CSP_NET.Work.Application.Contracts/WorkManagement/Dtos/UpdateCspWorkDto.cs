using CSP_NET.Work.Common;
using CTIN.Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.WorkManagement;

public class UpdateCspWorkDto : CreateUpdateCspWorkDto, IHasConcurrencyStamp
{
    public CspWorkStatus? Status { get; set; }
    public List<CspWorkNoteDto>? Notes { get; set; }
    public string ConcurrencyStamp { get; set; }
}
