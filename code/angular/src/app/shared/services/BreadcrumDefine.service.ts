import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalizationService } from '@ctincsp/ng.core';
import { map } from 'rxjs';

@Injectable()
export class BreadcrumDefineService {

  constructor(private router: Router,private route: ActivatedRoute, private localizationService: LocalizationService) {}

  getBreadcrum(routeConfig: any){
    let child = routeConfig?.children?.length > 0 ? this.childMenuItem(routeConfig?.children) : [];

    const breadCrumb = {
      label: this.localizationService.instant(routeConfig.data?.name),
      items: child
    }

    const itemBreadCrumb = []
    itemBreadCrumb.push({ label: breadCrumb.label });
    itemBreadCrumb.push(...this.defineBreadCrum(breadCrumb.items))

    return itemBreadCrumb;
  }

  childMenuItem(child: any){
    const items = [];

    if(child?.length > 0){
      const currentRoute = this.route.snapshot;
      const childPath = currentRoute?.children[0]?.routeConfig;

      const childItem = child?.find(item => item?.path === childPath?.path);

      let childNode = this.childMenuItem(childItem?.children);

      const data = {
        label: this.localizationService.instant(childItem?.data?.name),
        routerLink: childItem?.path,
        items: childNode
      }

      items.push(data)
    }

    return items;
  }


  defineBreadCrum(breadCrumItem: any){
    const brcItem = [];
    if (breadCrumItem) {
      breadCrumItem.forEach(item => {
        brcItem.push({ label: item.label, routerLink: item.routerLink});
      });
    }

    return brcItem;
  }



}
