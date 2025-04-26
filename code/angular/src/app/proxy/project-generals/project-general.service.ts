import type { CreateUpdateProjectGeneralDto, GetAllProjectGeneralDto, ProjectGeneralDto, ProjectGeneralGetListInput } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';
import type { DropdownItem } from '../csp/category/category-management/models';

@Injectable({
  providedIn: 'root',
})
export class ProjectGeneralService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProjectGeneralDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectGeneralDto>({
      method: 'POST',
      url: '/api/app/project-general',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/project-general/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectGeneralDto>({
      method: 'GET',
      url: `/api/app/project-general/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDropdown = (input: GetAllProjectGeneralDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DropdownItem[]>({
      method: 'GET',
      url: '/api/app/project-general/dropdown',
      params: { filter: input.filter, sorting: input.sorting },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ProjectGeneralGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProjectGeneralDto>>({
      method: 'GET',
      url: '/api/app/project-general',
      params: { code: input.code, name: input.name, department: input.department, description: input.description, departmentID: input.departmentID, projectStateID: input.projectStateID, status: input.status, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  syncProject = (code: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectGeneralDto>({
      method: 'POST',
      url: '/api/app/project-general/sync-project',
      params: { code },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateProjectGeneralDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProjectGeneralDto>({
      method: 'PUT',
      url: `/api/app/project-general/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateRoot = (id: string, input: CreateUpdateProjectGeneralDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/project-general/${id}/root`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
