import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';
import type { RecordStatusCode } from '../../common/record-status-code.enum';
import type { NameValue } from '../../ctin/abp/models';

export interface CreateUpdateDepartmentTeamDto {
  code?: string;
  name?: string;
  description?: string;
  departmentID?: string;
  users: string[];
}

export interface DepartmentTeamDto extends EntityDto<string> {
  code?: string;
  name?: string;
  description?: string;
  departmentID?: string;
  departmentName?: string;
  status: RecordStatusCode;
  lstMember: NameValue<string>[];
}

export interface DepartmentTeamFilterInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  departmentID?: string;
}

export interface DepartmentTeamGetListInput extends PagedAndSortedResultRequestDto {
  code?: string;
  name?: string;
  departmentID?: string;
  status: RecordStatusCode;
  filter?: string;
}
