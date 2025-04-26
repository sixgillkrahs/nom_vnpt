import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';

export interface CreateUpdateProjectRoleDto {
  code?: string;
  name?: string;
  domain?: string;
  status: boolean;
}

export interface GetAllProjectRoleDto {
  filter?: string;
  sorting?: string;
}

export interface GetListProjectRoleDto extends PagedAndSortedResultRequestDto {
  code?: string;
  name?: string;
  domain?: string;
  status?: boolean;
  filter?: string;
}

export interface ProjectRoleDto extends EntityDto<string> {
  code?: string;
  name?: string;
  domain?: string;
  status: boolean;
}
