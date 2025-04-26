import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';
import type { WorkAcceptStatus } from '../../common/work-accept-status.enum';

export interface ConfigWorkAcceptDto extends EntityDto<string> {
  workID?: string;
  userAccept1?: string;
  dateAccept1?: string;
  userAccept2?: string;
  dateAccept2?: string;
  statusAccept1: WorkAcceptStatus;
  statusAccept2: WorkAcceptStatus;
  accept2: boolean;
}

export interface ConfigWorkAcceptGetListInput extends PagedAndSortedResultRequestDto {
  workID?: string;
  userAccept1?: string;
  dateAccept1?: string;
  userAccept2?: string;
  dateAccept2?: string;
  statusAccept1: WorkAcceptStatus;
  statusAccept2: WorkAcceptStatus;
  accept2: boolean;
  filter?: string;
}

export interface CreateUpdateConfigWorkAcceptDto {
  workID?: string;
  userAccept1?: string;
  dateAccept1?: string;
  userAccept2?: string;
  dateAccept2?: string;
  statusAccept1: WorkAcceptStatus;
  statusAccept2: WorkAcceptStatus;
  accept2: boolean;
}
