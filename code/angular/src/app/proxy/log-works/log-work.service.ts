import type { CreateUpdateLogWorkDto, LogWorkDto, LogWorkGetListInput } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class LogWorkService {
  apiName = 'Default';
  

  create = (input: CreateUpdateLogWorkDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LogWorkDto>({
      method: 'POST',
      url: '/api/app/log-work',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/log-work/${id}`,
    },
    { apiName: this.apiName,...config });
  

  findByWorkId = (WorkId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LogWorkDto>({
      method: 'POST',
      url: `/api/app/log-work/find-by-work-id/${WorkId}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LogWorkDto>({
      method: 'GET',
      url: `/api/app/log-work/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: LogWorkGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LogWorkDto>>({
      method: 'GET',
      url: '/api/app/log-work',
      params: { workId: input.workId, userId: input.userId, time: input.time, date: input.date, description: input.description, statusLogWork: input.statusLogWork, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateLogWorkDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LogWorkDto>({
      method: 'PUT',
      url: `/api/app/log-work/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
