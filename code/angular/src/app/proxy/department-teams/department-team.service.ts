import type { CreateUpdateDepartmentTeamDto, DepartmentTeamDto, DepartmentTeamFilterInput, DepartmentTeamGetListInput } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DepartmentTeamService {
  apiName = 'Default';
  

  create = (input: CreateUpdateDepartmentTeamDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentTeamDto>({
      method: 'POST',
      url: '/api/app/department-team',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/department-team/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentTeamDto>({
      method: 'GET',
      url: `/api/app/department-team/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: DepartmentTeamGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepartmentTeamDto>>({
      method: 'GET',
      url: '/api/app/department-team',
      params: { code: input.code, name: input.name, departmentID: input.departmentID, status: input.status, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListClientIdByInput = (input: DepartmentTeamFilterInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepartmentTeamDto>>({
      method: 'GET',
      url: '/api/app/department-team/client-id',
      params: { filter: input.filter, departmentID: input.departmentID, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateDepartmentTeamDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentTeamDto>({
      method: 'PUT',
      url: `/api/app/department-team/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
