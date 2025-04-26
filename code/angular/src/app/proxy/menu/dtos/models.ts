import type { EntityDto } from '@ctincsp/ng.core';

export interface CreateUpdateMenuDto {
  routerLink?: string;
  url?: string;
  label?: string;
  order?: number;
  iconClass?: string;
  requiredPolicy?: string;
  layout?: string;
  parentId?: string;
  isGroup: boolean;
  clientId?: string;
}

export interface GetListMenuDto {
  clientId?: string;
}

export interface MenuDto extends EntityDto<string> {
  routerLink?: string;
  url?: string;
  label?: string;
  order?: number;
  iconClass?: string;
  requiredPolicy?: string;
  layout?: string;
  parentId?: string;
  isGroup: boolean;
  clientId?: string;
}
