import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';

export interface CreateUpdateProjectStateDto {
  code?: string;
  name?: string;
}

export interface GetAllProjectStateDto {
  filter?: string;
  sorting?: string;
}

export interface ProjectStateDto extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
}

export interface ProjectStateGetListInput extends PagedAndSortedResultRequestDto {
  code?: string;
  name?: string;
  filter?: string;
}
