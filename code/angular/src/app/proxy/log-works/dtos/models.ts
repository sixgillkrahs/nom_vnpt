import type { StatusLogWork } from '../../common/status-log-work.enum';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';

export interface CreateUpdateLogWorkDto {
  workId?: string;
  userId?: string;
  time?: string;
  date?: string;
  description?: string;
  statusLogWork: StatusLogWork;
}

export interface LogWorkDto extends EntityDto<string> {
  workId?: string;
  userId?: string;
  time?: string;
  date?: string;
  description?: string;
  statusLogWork: StatusLogWork;
}

export interface LogWorkGetListInput extends PagedAndSortedResultRequestDto {
  workId?: string;
  userId?: string;
  time?: string;
  date?: string;
  description?: string;
  statusLogWork: StatusLogWork;
  filter?: string;
}
