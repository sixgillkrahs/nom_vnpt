import type { DocumentDto, DocumentGetListInput } from './dtos/models';
import { Injectable } from '@angular/core';
import { RestService, Rest } from '@ctincsp/ng.core';
import type { PagedResultDto } from '@ctincsp/ng.core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';
import type { FileResult } from '../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  apiName = 'Default';
  

  deleteBytes = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/document/bytes',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  downloadImage = (imageName: string, mimeType: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileResult>({
      method: 'POST',
      url: '/api/app/document/download-image',
      params: { imageName, mimeType },
    },
    { apiName: this.apiName,...config });
  

  getAllByInput = (input: DocumentGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DocumentDto>>({
      method: 'GET',
      url: '/api/app/document',
      params: { filter: input.filter, updateBy: input.updateBy, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  saveBytes = (files: IFormFile[], WorkId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/document/save-bytes/${WorkId}`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
