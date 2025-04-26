import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { LocalizationService } from '@ctincsp/ng.core';
import { MenuItem } from 'primeng/api';
import { filter } from 'rxjs';
import { BreadcrumDefineService } from 'src/app/shared/services/BreadcrumDefine.service';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.scss'],
  providers: [BreadcrumDefineService]
})
export class ProjectManagementComponent implements OnInit {

  items: MenuItem[] | undefined;

  home: MenuItem | undefined;

  constructor(private router: Router,private route: ActivatedRoute, breadcrum: BreadcrumDefineService) {
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(event => {

      const root = this.router.routerState.snapshot.root;

      const routeConfig = root.children[0];

      this.items = breadcrum.getBreadcrum(routeConfig.children[0].routeConfig);
    });
   }

  ngOnInit() {
    this.home = { icon: 'pi pi-home', routerLink: '/' };
  }


}
