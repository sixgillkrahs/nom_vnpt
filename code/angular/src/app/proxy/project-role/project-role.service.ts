import type { CreateUpdateProjectRoleDto, GetAllProjectRoleDto, GetListProjectRoleDto, ProjectRoleDto } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';
import type { DropdownItem } from '../csp/category/category-management/models';

@Injectable({
  providedIn: 'root',
})
export class ProjectRoleService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProjectRoleDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectRoleDto>({
      method: 'POST',
      url: '/api/app/project-role',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/project-role/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectRoleDto>({
      method: 'GET',
      url: `/api/app/project-role/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDropdown = (input: GetAllProjectRoleDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DropdownItem[]>({
      method: 'GET',
      url: '/api/app/project-role/dropdown',
      params: { filter: input.filter, sorting: input.sorting },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetListProjectRoleDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProjectRoleDto>>({
      method: 'GET',
      url: '/api/app/project-role',
      params: { code: input.code, name: input.name, domain: input.domain, status: input.status, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateProjectRoleDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectRoleDto>({
      method: 'PUT',
      url: `/api/app/project-role/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateRoot = (id: string, input: CreateUpdateProjectRoleDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectRoleDto>({
      method: 'PUT',
      url: `/api/app/project-role/${id}/root`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
