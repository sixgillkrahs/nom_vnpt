import type { CreateUpdateProjectStateDto, GetAllProjectStateDto, ProjectStateDto, ProjectStateGetListInput } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';
import type { DropdownItemWork } from '../common/models';

@Injectable({
  providedIn: 'root',
})
export class ProjectStateService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProjectStateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectStateDto>({
      method: 'POST',
      url: '/api/app/project-state',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/project-state/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectStateDto>({
      method: 'GET',
      url: `/api/app/project-state/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDropdown = (input: GetAllProjectStateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DropdownItemWork[]>({
      method: 'GET',
      url: '/api/app/project-state/dropdown',
      params: { filter: input.filter, sorting: input.sorting },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ProjectStateGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProjectStateDto>>({
      method: 'GET',
      url: '/api/app/project-state',
      params: { code: input.code, name: input.name, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateProjectStateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectStateDto>({
      method: 'PUT',
      url: `/api/app/project-state/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateRoot = (id: string, input: CreateUpdateProjectStateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/project-state/${id}/root`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
