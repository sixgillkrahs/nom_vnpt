using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.Common;

public static class CspWorkConsts
{
    public const int MaxCodeLength = 128;
    public const int MaxNameLength = 256;
    public const int MaxDescriptionLength = 500;
    public const int MaxDurationLength = 50;
    public const int MaxPriorityLength = 50;
    public const int MaxComplextityLength = 50;
    public const int MaxDegreeOfImportantLength = 50;

}

public enum CspWorkStatus
{
    New = RecordStatusCode.New,
    Rejected = RecordStatusCode.reject,
    Progressing = RecordStatusCode.progressing,
    Processed = RecordStatusCode.progressed,
    PendingApproval = RecordStatusCode.approvering,
    Approved = RecordStatusCode.approved,
    Completed = RecordStatusCode.complete,
    Overdue = RecordStatusCode.overDue
}
public enum CspWorkAction
{
    StartProgress,
    Reject,
    HandOver
}