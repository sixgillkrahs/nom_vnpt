import { mapEnumToOptions } from '@ctincsp/ng.core';

export enum CspWorkAction {
  StartProgress = 0,
  Reject = 1,
  HandOver = 2,
}

export const cspWorkActionOptions = mapEnumToOptions(CspWorkAction);
