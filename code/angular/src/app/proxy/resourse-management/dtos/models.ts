import type { EntityDto, PagedAndSortedResultRequestDto } from '@ctincsp/ng.core';

export interface DocumentDto extends EntityDto<string> {
  name?: string;
  workId?: string;
  updateDate?: string;
  updateBy?: string;
  fileSize: number;
  fileUrl?: string;
  mimeType?: string;
}

export interface DocumentGetListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  updateBy?: string;
}
