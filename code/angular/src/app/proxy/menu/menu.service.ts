import type { CreateUpdateMenuDto, GetListMenuDto, MenuDto } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMenuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuDto>({
      method: 'POST',
      url: '/api/app/menu',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/menu/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuDto>({
      method: 'GET',
      url: `/api/app/menu/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetListMenuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MenuDto>>({
      method: 'GET',
      url: '/api/app/menu',
      params: { clientId: input.clientId },
    },
    { apiName: this.apiName,...config });
  

  getListClientId = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string[]>({
      method: 'GET',
      url: '/api/app/menu/client-id',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateMenuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuDto>({
      method: 'PUT',
      url: `/api/app/menu/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
