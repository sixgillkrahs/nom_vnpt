import type { ConfigWorkAcceptDto, ConfigWorkAcceptGetListInput, CreateUpdateConfigWorkAcceptDto } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class ConfigWorkAcceptService {
  apiName = 'Default';
  

  create = (input: CreateUpdateConfigWorkAcceptDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ConfigWorkAcceptDto>({
      method: 'POST',
      url: '/api/app/config-work-accept',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/config-work-accept/${id}`,
    },
    { apiName: this.apiName,...config });
  

  findByWorkId = (WorkId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ConfigWorkAcceptDto>({
      method: 'POST',
      url: `/api/app/config-work-accept/find-by-work-id/${WorkId}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ConfigWorkAcceptDto>({
      method: 'GET',
      url: `/api/app/config-work-accept/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ConfigWorkAcceptGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ConfigWorkAcceptDto>>({
      method: 'GET',
      url: '/api/app/config-work-accept',
      params: { workID: input.workID, userAccept1: input.userAccept1, dateAccept1: input.dateAccept1, userAccept2: input.userAccept2, dateAccept2: input.dateAccept2, statusAccept1: input.statusAccept1, statusAccept2: input.statusAccept2, accept2: input.accept2, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateConfigWorkAcceptDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ConfigWorkAcceptDto>({
      method: 'PUT',
      url: `/api/app/config-work-accept/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
