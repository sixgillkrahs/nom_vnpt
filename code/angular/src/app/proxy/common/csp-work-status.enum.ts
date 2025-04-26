import { mapEnumToOptions } from '@ctincsp/ng.core';

export enum CspWorkStatus {
  New = 5,
  Rejected = 11,
  PendingApproval = 39,
  Approved = 40,
  Progressing = 59,
  Processed = 60,
  Completed = 102,
  Overdue = 103,
}

export const cspWorkStatusOptions = mapEnumToOptions(CspWorkStatus);
