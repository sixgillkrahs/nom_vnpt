import type { CreateCspWorkDto, CspWorkDto, CspWorkGetListInput, GetAllProjectMemberDto, UpdateCspWorkDto } from './models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';
import type { DropdownItemWork } from '../common/models';

@Injectable({
  providedIn: 'root',
})
export class CspWorkService {
  apiName = 'Default';
  

  create = (input: CreateCspWorkDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto>({
      method: 'POST',
      url: '/api/app/csp-work',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/csp-work/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto>({
      method: 'GET',
      url: `/api/app/csp-work/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getChildren = (parentId: string, recusive?: boolean, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto[]>({
      method: 'GET',
      url: `/api/app/csp-work/children/${parentId}`,
      params: { recusive },
    },
    { apiName: this.apiName,...config });
  

  getDropdown = (input: GetAllProjectMemberDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DropdownItemWork[]>({
      method: 'GET',
      url: '/api/app/csp-work/dropdown',
      params: { filter: input.filter, sorting: input.sorting },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: CspWorkGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CspWorkDto>>({
      method: 'GET',
      url: '/api/app/csp-work',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount, sorting: input.sorting, keyword: input.keyword, workCode: input.workCode, taskCode: input.taskCode, priority: input.priority, projectId: input.projectId, parentId: input.parentId, status: input.status, onlyRootNode: input.onlyRootNode, childIds: input.childIds, ownerId: input.ownerId, assignerId: input.assignerId, performerId: input.performerId, collaboratorId: input.collaboratorId, userId: input.userId },
    },
    { apiName: this.apiName,...config });
  

  handOver = (id: string, perFormerId: string, note: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto>({
      method: 'PUT',
      url: `/api/app/csp-work/${id}/hand-over/${perFormerId}`,
      params: { note },
    },
    { apiName: this.apiName,...config });
  

  startProgress = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto>({
      method: 'PUT',
      url: `/api/app/csp-work/${id}/start-progress`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCspWorkDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto>({
      method: 'PUT',
      url: `/api/app/csp-work/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateLisWorkOfUser = (ids: string[], perFormerId: string, note: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CspWorkDto[]>({
      method: 'PUT',
      url: `/api/app/csp-work/lis-work-of-user/${perFormerId}`,
      params: { note },
      body: ids,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
