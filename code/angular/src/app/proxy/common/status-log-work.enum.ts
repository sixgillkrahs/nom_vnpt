import { mapEnumToOptions } from '@ctincsp/ng.core';

export enum StatusLogWork {
  None = 0,
  Delete = 1,
}

export const statusLogWorkOptions = mapEnumToOptions(StatusLogWork);
