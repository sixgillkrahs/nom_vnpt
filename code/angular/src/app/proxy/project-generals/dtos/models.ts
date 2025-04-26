import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';

export interface CreateUpdateProjectGeneralDto {
  code?: string;
  name?: string;
  departmentID?: string;
  description?: string;
  projectStateID?: string;
  status: boolean;
}

export interface GetAllProjectGeneralDto {
  filter?: string;
  sorting?: string;
}

export interface ProjectGeneralDto extends EntityDto<string> {
  code?: string;
  name?: string;
  departmentID?: string;
  department: _OrganizationUnit;
  description?: string;
  projectStateID?: string;
  projectState: _ProjectStateDto;
  status: boolean;
}

export interface ProjectGeneralGetListInput extends PagedAndSortedResultRequestDto {
  code?: string;
  name?: string;
  department?: string;
  description?: string;
  departmentID?: string;
  projectStateID?: string;
  status?: boolean;
  filter?: string;
}

export interface _OrganizationUnit {
  displayName?: string;
}

export interface _ProjectStateDto {
  code?: string;
  name?: string;
}
