import type { AuditedEntityDto } from '@ctincsp/ng.core';
import type { CspWorkStatus } from '../common/csp-work-status.enum';
import type { CspWorkAction } from '../common/csp-work-action.enum';

export interface CreateCspWorkDto extends CreateUpdateCspWorkDto {
  parentId?: string;
}

export interface CreateUpdateCspWorkDto {
  workCode: string;
  name: string;
  taskCode: string;
  startDate?: string;
  dueDate?: string;
  progress?: number;
  duration?: string;
  priority: string;
  complexity?: string;
  degreeOfImportant?: string;
  description: string;
  owner?: string;
  assigner?: string;
  performer?: string;
  collaborators?: string;
  checkList: CspWorkCheckListDto[];
  projectId: string;
  restrictComplete: boolean;
}

export interface CspWorkCheckListDto {
  title?: string;
  isCompleted: boolean;
}

export interface CspWorkDto extends AuditedEntityDto<string> {
  workCode?: string;
  taskCode?: string;
  startDate?: string;
  startProgressDate?: string;
  dueDate?: string;
  progress: number;
  duration?: string;
  priority?: string;
  complexity?: string;
  degreeOfImportant?: string;
  description?: string;
  owner?: string;
  ownerName?: string;
  assigner?: string;
  assignerName?: string;
  performer?: string;
  performerName?: string;
  collaborators?: string;
  collaboratorNames?: string;
  members?: string;
  restrictComplete: boolean;
  status: CspWorkStatus;
  checkList: CspWorkCheckListDto[];
  notes: CspWorkNoteDto[];
  projectId?: string;
  projectName?: string;
  name?: string;
  fullName?: string;
  code?: string;
  level: number;
  parentId?: string;
  concurrencyStamp?: string;
}

export interface CspWorkGetListInput {
  maxResultCount?: number;
  skipCount?: number;
  sorting?: string;
  keyword?: string;
  workCode?: string;
  taskCode?: string;
  priority?: string;
  projectId?: string;
  parentId?: string;
  status?: CspWorkStatus;
  onlyRootNode: boolean;
  childIds: string[];
  ownerId?: string;
  assignerId?: string;
  performerId?: string;
  collaboratorId?: string;
  userId?: string;
}

export interface CspWorkNoteDto {
  userId?: string;
  userName?: string;
  name?: string;
  surname?: string;
  action: CspWorkAction;
  content?: string;
}

export interface GetAllProjectMemberDto {
  filter?: string;
  sorting?: string;
}

export interface UpdateCspWorkDto extends CreateUpdateCspWorkDto {
  status?: CspWorkStatus;
  notes: CspWorkNoteDto[];
  concurrencyStamp?: string;
}
