import { mapEnumToOptions } from '@ctincsp/ng.core';

export enum WorkAcceptStatus {
  None = 0,
  Accept = 1,
  Reject = 2,
}

export const workAcceptStatusOptions = mapEnumToOptions(WorkAcceptStatus);
